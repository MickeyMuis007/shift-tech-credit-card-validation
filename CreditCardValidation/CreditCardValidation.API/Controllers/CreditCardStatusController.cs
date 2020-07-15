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
using CreditCardValidation.Common.Models.CreditCardStatuses;
using CreditCardValidation.Domain.CreditCardStatusAggregate;
using SharedKernel.Interfaces;
using SharedKernel.Models;
using SharedKernel.Extensions;

namespace CreditCardValidation.API.Controllers
{
	[Produces("application/json", "application/vnd.learnex.hateoas+json")]
	[ApiController]
	[Route("api/creditCardStatus")]
	[ApiConventionType(typeof(CustomConventions))]
	public class CreditCardStatusController : ControllerBase
	{
		private readonly CreditCardStatusBuilder _creditCardStatusBuilder;
		private readonly IPropertyMappingService _propertyMappingService;
		private readonly IPropertyCheckerService _propertyCheckerService;

		public CreditCardStatusController(CreditCardStatusBuilder builder, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService)
		{
			_creditCardStatusBuilder = builder;
			_propertyMappingService = propertyMappingService;
			_propertyCheckerService = propertyCheckerService;
		}

		[HttpGet(Name = "GetCreditCardStatusList")]
		[HttpHead]
		public async Task<IActionResult> Get([FromQuery] CreditCardStatusQueryParams query)
		{
			if (!_propertyMappingService.ValidMappingExistsFor<CreditCardStatusDTO, CreditCardStatus>(query.OrderBy))
			{
				return BadRequest("OrderBy invalid");
			}

			if (!_propertyCheckerService.TypeHasProperties<CreditCardStatusDTO>(query.Fields))
			{
				return BadRequest("Fields are invalid");
			}

			var results = await _creditCardStatusBuilder.Build().GetAll(query);
			var responseWrapper = CreateResponseWrapper(results, query);
			return Ok(responseWrapper);
		}

		[HttpGet("{id:guid}", Name = "GetCreditCardStatus")]
		[HttpHead("{id:guid}")]
		public async Task<ActionResult<ResponseWrapper<IDictionary<string, object>, MetaData>>> Get(Guid id, string fields, [FromHeader(Name = "Accept")] string mediaType)
		{
			if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
			{
				return BadRequest("Invalid media type");
			}

			if (!_propertyCheckerService.TypeHasProperties<CreditCardStatusDTO>(fields))
			{
				return BadRequest("Fields are invalid");
			}

			var creditCardStatus = await _creditCardStatusBuilder.Build().GetById(id);
			if (creditCardStatus == null) return NotFound();

			var responseWrapper = CreateResponseWrapper(creditCardStatus, StatusCodes.Status200OK, fields, mediaType);
			return Ok(responseWrapper);
		}

		[HttpPost(Name = "CreateCreditCardStatus")]
		public async Task<ActionResult<ResponseWrapper<IEnumerable<IDictionary<string, object>>, MetaData>>> Insert([FromBody] CreditCardStatusInsertDTO model, string fields)
		{
			var creditCardStatus = await _creditCardStatusBuilder.Copy(model).Build().Insert();
			var responseWrapper = CreateResponseWrapper(creditCardStatus, StatusCodes.Status201Created, fields, null);

			return CreatedAtRoute("GetCreditCardStatus", new { Id = creditCardStatus.Id}, responseWrapper);
		}

		[HttpPut("{id:guid}", Name = "UpdateCreditCardStatus")]
		public async Task<ActionResult> Update (Guid id, [FromBody] CreditCardStatusUpdateDTO dto, [FromQuery] bool upsert)
		{
			var creditCardStatusDTO = await _creditCardStatusBuilder.Copy(dto).SetId(id).Build().Update();
			if (creditCardStatusDTO == null)
			{
				if (upsert)
				{
					creditCardStatusDTO = await _creditCardStatusBuilder.Copy(dto).SetId(id).Build().Insert();
					return CreatedAtRoute("GetCreditCardStatus", new { Id = creditCardStatusDTO.Id }, creditCardStatusDTO);
				}
				else
				{
					return NotFound();
				}
			}
			return NoContent();
		}

		[HttpDelete("{id:guid}", Name = "DeleteCreditCardStatus")]
		public async Task<ActionResult> Delete(Guid id)
		{
			var creditCardStatusDTO = await _creditCardStatusBuilder.SetId(id).Build().Delete();
			if (creditCardStatusDTO == null) return NotFound();

			return NoContent();
		}

		[HttpOptions]
		public ActionResult GetOptions()
		{
			Response.Headers.Add("Allow","GET,OPTIONS,POST,HEAD,PUT,DELETE");
			return Ok();
		}

		private string CreateCreditCardStatusResourceUri(CreditCardStatusQueryParams queryParams, ResourceUriType type)
		{
			switch (type)
			{
				case ResourceUriType.PreviousPage:
					return Url.Link("GetCreditCardStatusList",
						new
						{
							pageNumber = queryParams.PageNumber - 1,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
				case ResourceUriType.NextPage:
					return Url.Link("GetCreditCardStatusList",
						new
						{
							pageNumber = queryParams.PageNumber + 1,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
				case ResourceUriType.Current:
				default:
					return Url.Link("GetCreditCardStatusList",
						new
						{
							pageNumber = queryParams.PageNumber,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
			}
		}
		private ResponseWrapper<IEnumerable<IDictionary<string, object>>, ListMetaData > CreateResponseWrapper(PagedList<CreditCardStatusDTO> list, CreditCardStatusQueryParams query)
		{
			var previousPageLink = list.HasPrevious ?
				CreateCreditCardStatusResourceUri(query, ResourceUriType.PreviousPage) : null;

			var nextPageLink = list.HasNext ?
				CreateCreditCardStatusResourceUri(query, ResourceUriType.NextPage) : null;

			var listMetaData = new ListMetaData
			{
				TotalCount = list.TotalCount,
				PageSize = list.PageSize,
				CurrentPage = list.CurrentPage,
				TotalPages = list.TotalPages,
				OrderBy = query.OrderBy,
				SearchQuery = query.SearchQuery,
				Links = CreateLinksForCreditCardStatusList (query, list.HasNext, list.HasPrevious)
			};

			var shapedCreditCardStatus = ((IEnumerable<CreditCardStatusDTO>) list).ShapeData(query.Fields).Select(creditCardStatus =>
				{
					var creditCardStatusAsDictionary = creditCardStatus as IDictionary<string, object>;
					var creditCardStatusLinks = CreateLinksForCreditCardStatus((Guid) creditCardStatusAsDictionary["Id"], null);
					creditCardStatusAsDictionary.Add("links", creditCardStatusLinks);
					return creditCardStatusAsDictionary;
				});

			var responseWrapper = new ResponseWrapper<IEnumerable<IDictionary<string, object>>, ListMetaData >
			{
				results = shapedCreditCardStatus,
				MetaData = listMetaData
			};
			Response.Headers.Add("X-Paginaton",JsonSerializer.Serialize(listMetaData));
			return responseWrapper;
		}
		private ResponseWrapper<IDictionary<string, object>, MetaData> CreateResponseWrapper(CreditCardStatusDTO creditCardStatus, int statusCode, string fields, string mediaType)
		{
			var metaData = new MetaData
			{
				Message = "Successfully created",
				StatusCode = statusCode
			};

			var responseWrapper = new ResponseWrapper<IDictionary<string, object>, MetaData>
				{
					results = creditCardStatus.ShapeData(fields) as IDictionary<string, object>,
					MetaData = metaData
				};

			if (mediaType == "application/vnd.learnex.hateoas+json")
			{
				responseWrapper.results.Add("links", CreateLinksForCreditCardStatus(creditCardStatus.Id, fields));
			}
			return responseWrapper;
		}
		private IEnumerable<LinkDTO> CreateLinksForCreditCardStatus(Guid id, string fields)
		{
			var links = new List<LinkDTO>();
			if (string.IsNullOrWhiteSpace(fields))
			{
				links.Add(new LinkDTO(Url.Link("GetCreditCardStatus", new { id }), "self","GET"));
			}
			else
			{
				links.Add(new LinkDTO(Url.Link("GetCreditCardStatus", new { id, fields }), "update_creditCardStatus", "GET"));
			}

			links.Add(new LinkDTO(Url.Link("DeleteCreditCardStatus", new { id }), "delete_creditCardStatus", "DELETE"));
			links.Add(new LinkDTO(Url.Link("UpdateCreditCardStatus", new { id }), "update_creditCardStatus", "PUT"));
			return links;
		}
		private IEnumerable<LinkDTO> CreateLinksForCreditCardStatusList(CreditCardStatusQueryParams queryParams, bool hasNext, bool hasPrevious)
		{
			var links = new List<LinkDTO>();
			links.Add(new LinkDTO(CreateCreditCardStatusResourceUri(queryParams, ResourceUriType.Current), "self", "GET"));

			if (hasNext)
			{
				links.Add(new LinkDTO(CreateCreditCardStatusResourceUri(queryParams, ResourceUriType.NextPage), "nextPage", "GET"));
			}

			if (hasPrevious)
			{
				links.Add(new LinkDTO(CreateCreditCardStatusResourceUri(queryParams, ResourceUriType.PreviousPage), "previousPage", "GET"));
			}
			return links;
		}
	}
}