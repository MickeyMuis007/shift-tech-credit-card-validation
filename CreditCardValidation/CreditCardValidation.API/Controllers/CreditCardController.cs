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
using CreditCardValidation.Common.Models.CreditCards;
using CreditCardValidation.Domain.CreditCardAggregate;
using SharedKernel.Interfaces;
using SharedKernel.Models;
using SharedKernel.Extensions;

namespace CreditCardValidation.API.Controllers
{
	[Produces("application/json", "application/vnd.learnex.hateoas+json")]
	[ApiController]
	[Route("api/creditCard")]
	[ApiConventionType(typeof(CustomConventions))]
	public class CreditCardController : ControllerBase
	{
		private readonly CreditCardBuilder _creditCardBuilder;
		private readonly IPropertyMappingService _propertyMappingService;
		private readonly IPropertyCheckerService _propertyCheckerService;

		public CreditCardController(CreditCardBuilder builder, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService)
		{
			_creditCardBuilder = builder;
			_propertyMappingService = propertyMappingService;
			_propertyCheckerService = propertyCheckerService;
		}

		[HttpGet(Name = "GetCreditCardList")]
		[HttpHead]
		public async Task<IActionResult> Get([FromQuery] CreditCardQueryParams query)
		{
			if (!_propertyMappingService.ValidMappingExistsFor<CreditCardDTO, CreditCard>(query.OrderBy))
			{
				return BadRequest("OrderBy invalid");
			}

			if (!_propertyCheckerService.TypeHasProperties<CreditCardDTO>(query.Fields))
			{
				return BadRequest("Fields are invalid");
			}

			var results = await _creditCardBuilder.Build().GetAll(query);
			var responseWrapper = CreateResponseWrapper(results, query);
			return Ok(responseWrapper);
		}

		[HttpGet("{id:guid}", Name = "GetCreditCard")]
		[HttpHead("{id:guid}")]
		public async Task<ActionResult<ResponseWrapper<IDictionary<string, object>, MetaData>>> Get(Guid id, string fields, [FromHeader(Name = "Accept")] string mediaType)
		{
			if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
			{
				return BadRequest("Invalid media type");
			}

			if (!_propertyCheckerService.TypeHasProperties<CreditCardDTO>(fields))
			{
				return BadRequest("Fields are invalid");
			}

			var creditCard = await _creditCardBuilder.Build().GetById(id);
			if (creditCard == null) return NotFound();

			var responseWrapper = CreateResponseWrapper(creditCard, StatusCodes.Status200OK, fields, mediaType);
			return Ok(responseWrapper);
		}

		[HttpPost(Name = "CreateCreditCard")]
		public async Task<ActionResult<ResponseWrapper<IEnumerable<IDictionary<string, object>>, MetaData>>> Insert([FromBody] CreditCardInsertDTO model, string fields)
		{
			var creditCard = await _creditCardBuilder.Copy(model).Build().Insert();
			var responseWrapper = CreateResponseWrapper(creditCard, StatusCodes.Status201Created, fields, null);

			return CreatedAtRoute("GetCreditCard", new { Id = creditCard.Id}, responseWrapper);
		}

		[HttpPut("{id:guid}", Name = "UpdateCreditCard")]
		public async Task<ActionResult> Update (Guid id, [FromBody] CreditCardUpdateDTO dto, [FromQuery] bool upsert)
		{
			var creditCardDTO = await _creditCardBuilder.Copy(dto).SetId(id).Build().Update();
			if (creditCardDTO == null)
			{
				if (upsert)
				{
					creditCardDTO = await _creditCardBuilder.Copy(dto).SetId(id).Build().Insert();
					return CreatedAtRoute("GetCreditCard", new { Id = creditCardDTO.Id }, creditCardDTO);
				}
				else
				{
					return NotFound();
				}
			}
			return NoContent();
		}

		[HttpDelete("{id:guid}", Name = "DeleteCreditCard")]
		public async Task<ActionResult> Delete(Guid id)
		{
			var creditCardDTO = await _creditCardBuilder.SetId(id).Build().Delete();
			if (creditCardDTO == null) return NotFound();

			return NoContent();
		}

		[HttpOptions]
		public ActionResult GetOptions()
		{
			Response.Headers.Add("Allow","GET,OPTIONS,POST,HEAD,PUT,DELETE");
			return Ok();
		}

		private string CreateCreditCardResourceUri(CreditCardQueryParams queryParams, ResourceUriType type)
		{
			switch (type)
			{
				case ResourceUriType.PreviousPage:
					return Url.Link("GetCreditCardList",
						new
						{
							pageNumber = queryParams.PageNumber - 1,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
				case ResourceUriType.NextPage:
					return Url.Link("GetCreditCardList",
						new
						{
							pageNumber = queryParams.PageNumber + 1,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
				case ResourceUriType.Current:
				default:
					return Url.Link("GetCreditCardList",
						new
						{
							pageNumber = queryParams.PageNumber,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
			}
		}
		private ResponseWrapper<IEnumerable<IDictionary<string, object>>, ListMetaData > CreateResponseWrapper(PagedList<CreditCardDTO> list, CreditCardQueryParams query)
		{
			var previousPageLink = list.HasPrevious ?
				CreateCreditCardResourceUri(query, ResourceUriType.PreviousPage) : null;

			var nextPageLink = list.HasNext ?
				CreateCreditCardResourceUri(query, ResourceUriType.NextPage) : null;

			var listMetaData = new ListMetaData
			{
				TotalCount = list.TotalCount,
				PageSize = list.PageSize,
				CurrentPage = list.CurrentPage,
				TotalPages = list.TotalPages,
				OrderBy = query.OrderBy,
				SearchQuery = query.SearchQuery,
				Links = CreateLinksForCreditCardList (query, list.HasNext, list.HasPrevious)
			};

			var shapedCreditCard = ((IEnumerable<CreditCardDTO>) list).ShapeData(query.Fields).Select(creditCard =>
				{
					var creditCardAsDictionary = creditCard as IDictionary<string, object>;
					var creditCardLinks = CreateLinksForCreditCard((Guid) creditCardAsDictionary["Id"], null);
					creditCardAsDictionary.Add("links", creditCardLinks);
					return creditCardAsDictionary;
				});

			var responseWrapper = new ResponseWrapper<IEnumerable<IDictionary<string, object>>, ListMetaData >
			{
				results = shapedCreditCard,
				MetaData = listMetaData
			};
			Response.Headers.Add("X-Paginaton",JsonSerializer.Serialize(listMetaData));
			return responseWrapper;
		}
		private ResponseWrapper<IDictionary<string, object>, MetaData> CreateResponseWrapper(CreditCardDTO creditCard, int statusCode, string fields, string mediaType)
		{
			var metaData = new MetaData
			{
				Message = "Successfully created",
				StatusCode = statusCode
			};

			var responseWrapper = new ResponseWrapper<IDictionary<string, object>, MetaData>
				{
					results = creditCard.ShapeData(fields) as IDictionary<string, object>,
					MetaData = metaData
				};

			if (mediaType == "application/vnd.learnex.hateoas+json")
			{
				responseWrapper.results.Add("links", CreateLinksForCreditCard(creditCard.Id, fields));
			}
			return responseWrapper;
		}
		private IEnumerable<LinkDTO> CreateLinksForCreditCard(Guid id, string fields)
		{
			var links = new List<LinkDTO>();
			if (string.IsNullOrWhiteSpace(fields))
			{
				links.Add(new LinkDTO(Url.Link("GetCreditCard", new { id }), "self","GET"));
			}
			else
			{
				links.Add(new LinkDTO(Url.Link("GetCreditCard", new { id, fields }), "update_creditCard", "GET"));
			}

			links.Add(new LinkDTO(Url.Link("DeleteCreditCard", new { id }), "delete_creditCard", "DELETE"));
			links.Add(new LinkDTO(Url.Link("UpdateCreditCard", new { id }), "update_creditCard", "PUT"));
			return links;
		}
		private IEnumerable<LinkDTO> CreateLinksForCreditCardList(CreditCardQueryParams queryParams, bool hasNext, bool hasPrevious)
		{
			var links = new List<LinkDTO>();
			links.Add(new LinkDTO(CreateCreditCardResourceUri(queryParams, ResourceUriType.Current), "self", "GET"));

			if (hasNext)
			{
				links.Add(new LinkDTO(CreateCreditCardResourceUri(queryParams, ResourceUriType.NextPage), "nextPage", "GET"));
			}

			if (hasPrevious)
			{
				links.Add(new LinkDTO(CreateCreditCardResourceUri(queryParams, ResourceUriType.PreviousPage), "previousPage", "GET"));
			}
			return links;
		}
	}
}