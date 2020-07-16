using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using API.Configurations.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CreditCardValidation.API.Enums;
using CreditCardValidation.Common.Models.CreditCardProviders;
using CreditCardValidation.Domain.CreditCardProviderAggregate;
using SharedKernel.Interfaces;
using SharedKernel.Models;
using SharedKernel.Extensions;

namespace CreditCardValidation.API.Controllers
{
	[Produces("application/json", "application/vnd.learnex.hateoas+json")]
	[ApiController]
	[Route("api/creditCardProvider")]
	[ApiConventionType(typeof(CustomConventions))]
	public class CreditCardProviderController : ControllerBase
	{
		private readonly CreditCardProviderBuilder _creditCardProviderBuilder;
		private readonly IPropertyMappingService _propertyMappingService;
		private readonly IPropertyCheckerService _propertyCheckerService;

		public CreditCardProviderController(CreditCardProviderBuilder builder, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService)
		{
			_creditCardProviderBuilder = builder;
			_propertyMappingService = propertyMappingService;
			_propertyCheckerService = propertyCheckerService;
		}

		[HttpGet(Name = "GetCreditCardProviderList")]
		[HttpHead]
		public async Task<IActionResult> Get([FromQuery] CreditCardProviderQueryParams query)
		{
			if (!_propertyMappingService.ValidMappingExistsFor<CreditCardProviderDTO, CreditCardProvider>(query.OrderBy))
			{
				return BadRequest("OrderBy invalid");
			}

			if (!_propertyCheckerService.TypeHasProperties<CreditCardProviderDTO>(query.Fields))
			{
				return BadRequest("Fields are invalid");
			}

			var results = await _creditCardProviderBuilder.Build().GetAll(query);
			var responseWrapper = CreateResponseWrapper(results, query);
			return Ok(responseWrapper);
		}

		[HttpGet("{id:guid}", Name = "GetCreditCardProvider")]
		[HttpHead("{id:guid}")]
		public async Task<ActionResult<ResponseWrapper<IDictionary<string, object>, MetaData>>> Get(Guid id, string fields, [FromHeader(Name = "Accept")] string mediaType)
		{
			if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
			{
				return BadRequest("Invalid media type");
			}

			if (!_propertyCheckerService.TypeHasProperties<CreditCardProviderDTO>(fields))
			{
				return BadRequest("Fields are invalid");
			}

			var creditCardProvider = await _creditCardProviderBuilder.Build().GetById(id);
			if (creditCardProvider == null) return NotFound();

			var responseWrapper = CreateResponseWrapper(creditCardProvider, StatusCodes.Status200OK, fields, mediaType);
			return Ok(responseWrapper);
		}

		[HttpPost(Name = "CreateCreditCardProvider")]
		public async Task<ActionResult<ResponseWrapper<IEnumerable<IDictionary<string, object>>, MetaData>>> Insert([FromBody] CreditCardProviderInsertDTO model, string fields)
		{
			var creditCardProvider = await _creditCardProviderBuilder.Copy(model).Build().Insert();
			var responseWrapper = CreateResponseWrapper(creditCardProvider, StatusCodes.Status201Created, fields, null);

			return CreatedAtRoute("GetCreditCardProvider", new { Id = creditCardProvider.Id}, responseWrapper);
		}

		[HttpPut("{id:guid}", Name = "UpdateCreditCardProvider")]
		public async Task<ActionResult> Update (Guid id, [FromBody] CreditCardProviderUpdateDTO dto, [FromQuery] bool upsert)
		{
			var creditCardProviderDTO = await _creditCardProviderBuilder.Copy(dto).SetId(id).Build().Update();
			if (creditCardProviderDTO == null)
			{
				if (upsert)
				{
					creditCardProviderDTO = await _creditCardProviderBuilder.Copy(dto).SetId(id).Build().Insert();
					return CreatedAtRoute("GetCreditCardProvider", new { Id = creditCardProviderDTO.Id }, creditCardProviderDTO);
				}
				else
				{
					return NotFound();
				}
			}
			return NoContent();
		}

		[HttpDelete("{id:guid}", Name = "DeleteCreditCardProvider")]
		public async Task<ActionResult> Delete(Guid id)
		{
			var creditCardProviderDTO = await _creditCardProviderBuilder.SetId(id).Build().Delete();
			if (creditCardProviderDTO == null) return NotFound();

			return NoContent();
		}

		[HttpOptions]
		public ActionResult GetOptions()
		{
			Response.Headers.Add("Allow","GET,OPTIONS,POST,HEAD,PUT,DELETE");
			return Ok();
		}

		private string CreateCreditCardProviderResourceUri(CreditCardProviderQueryParams queryParams, ResourceUriType type)
		{
			switch (type)
			{
				case ResourceUriType.PreviousPage:
					return Url.Link("GetCreditCardProviderList",
						new
						{
							pageNumber = queryParams.PageNumber - 1,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
				case ResourceUriType.NextPage:
					return Url.Link("GetCreditCardProviderList",
						new
						{
							pageNumber = queryParams.PageNumber + 1,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
				case ResourceUriType.Current:
				default:
					return Url.Link("GetCreditCardProviderList",
						new
						{
							pageNumber = queryParams.PageNumber,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
			}
		}
		private ResponseWrapper<IEnumerable<IDictionary<string, object>>, ListMetaData > CreateResponseWrapper(PagedList<CreditCardProviderDTO> list, CreditCardProviderQueryParams query)
		{
			var previousPageLink = list.HasPrevious ?
				CreateCreditCardProviderResourceUri(query, ResourceUriType.PreviousPage) : null;

			var nextPageLink = list.HasNext ?
				CreateCreditCardProviderResourceUri(query, ResourceUriType.NextPage) : null;

			var listMetaData = new ListMetaData
			{
				TotalCount = list.TotalCount,
				PageSize = list.PageSize,
				CurrentPage = list.CurrentPage,
				TotalPages = list.TotalPages,
				OrderBy = query.OrderBy,
				SearchQuery = query.SearchQuery,
				Links = CreateLinksForCreditCardProviderList (query, list.HasNext, list.HasPrevious)
			};

			var shapedCreditCardProvider = ((IEnumerable<CreditCardProviderDTO>) list).ShapeData(query.Fields).Select(creditCardProvider =>
				{
					var creditCardProviderAsDictionary = creditCardProvider as IDictionary<string, object>;
					var creditCardProviderLinks = CreateLinksForCreditCardProvider((Guid) creditCardProviderAsDictionary["Id"], null);
					creditCardProviderAsDictionary.Add("links", creditCardProviderLinks);
					return creditCardProviderAsDictionary;
				});

			var responseWrapper = new ResponseWrapper<IEnumerable<IDictionary<string, object>>, ListMetaData >
			{
				results = shapedCreditCardProvider,
				MetaData = listMetaData
			};
			Response.Headers.Add("X-Paginaton",JsonSerializer.Serialize(listMetaData));
			return responseWrapper;
		}
		private ResponseWrapper<IDictionary<string, object>, MetaData> CreateResponseWrapper(CreditCardProviderDTO creditCardProvider, int statusCode, string fields, string mediaType)
		{
			var metaData = new MetaData
			{
				Message = "Successfully created",
				StatusCode = statusCode
			};

			var responseWrapper = new ResponseWrapper<IDictionary<string, object>, MetaData>
				{
					results = creditCardProvider.ShapeData(fields) as IDictionary<string, object>,
					MetaData = metaData
				};

			if (mediaType == "application/vnd.learnex.hateoas+json")
			{
				responseWrapper.results.Add("links", CreateLinksForCreditCardProvider(creditCardProvider.Id, fields));
			}
			return responseWrapper;
		}
		private IEnumerable<LinkDTO> CreateLinksForCreditCardProvider(Guid id, string fields)
		{
			var links = new List<LinkDTO>();
			if (string.IsNullOrWhiteSpace(fields))
			{
				links.Add(new LinkDTO(Url.Link("GetCreditCardProvider", new { id }), "self","GET"));
			}
			else
			{
				links.Add(new LinkDTO(Url.Link("GetCreditCardProvider", new { id, fields }), "update_creditCardProvider", "GET"));
			}

			links.Add(new LinkDTO(Url.Link("DeleteCreditCardProvider", new { id }), "delete_creditCardProvider", "DELETE"));
			links.Add(new LinkDTO(Url.Link("UpdateCreditCardProvider", new { id }), "update_creditCardProvider", "PUT"));
			return links;
		}
		private IEnumerable<LinkDTO> CreateLinksForCreditCardProviderList(CreditCardProviderQueryParams queryParams, bool hasNext, bool hasPrevious)
		{
			var links = new List<LinkDTO>();
			links.Add(new LinkDTO(CreateCreditCardProviderResourceUri(queryParams, ResourceUriType.Current), "self", "GET"));

			if (hasNext)
			{
				links.Add(new LinkDTO(CreateCreditCardProviderResourceUri(queryParams, ResourceUriType.NextPage), "nextPage", "GET"));
			}

			if (hasPrevious)
			{
				links.Add(new LinkDTO(CreateCreditCardProviderResourceUri(queryParams, ResourceUriType.PreviousPage), "previousPage", "GET"));
			}
			return links;
		}
	}
}