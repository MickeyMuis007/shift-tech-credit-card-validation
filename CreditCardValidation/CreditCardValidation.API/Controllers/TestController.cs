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
using CreditCardValidation.Common.Models.TestModels;
using CreditCardValidation.Domain.TestAggregate;
using SharedKernel.Interfaces;
using SharedKernel.Models;
using SharedKernel.Extensions;

namespace CreditCardValidation.API.Controllers
{
	[Produces("application/json", "application/vnd.learnex.hateoas+json")]
	[ApiController]
	[Route("api/test")]
	[ApiConventionType(typeof(CustomConventions))]
	public class TestController : ControllerBase
	{
		private readonly TestBuilder _testBuilder;
		private readonly IPropertyMappingService _propertyMappingService;
		private readonly IPropertyCheckerService _propertyCheckerService;

		public TestController(TestBuilder builder, IPropertyMappingService propertyMappingService, IPropertyCheckerService propertyCheckerService)
		{
			_testBuilder = builder;
			_propertyMappingService = propertyMappingService;
			_propertyCheckerService = propertyCheckerService;
		}

		[HttpGet(Name = "GetTestList")]
		[HttpHead]
		public async Task<IActionResult> Get([FromQuery] TestQueryParams query)
		{
			if (!_propertyMappingService.ValidMappingExistsFor<TestDTO, Test>(query.OrderBy))
			{
				return BadRequest("OrderBy invalid");
			}

			if (!_propertyCheckerService.TypeHasProperties<TestDTO>(query.Fields))
			{
				return BadRequest("Fields are invalid");
			}

			var results = await _testBuilder.Build().GetAll(query);
			var responseWrapper = CreateResponseWrapper(results, query);
			return Ok(responseWrapper);
		}

		[HttpGet("{id:guid}", Name = "GetTest")]
		[HttpHead("{id:guid}")]
		public async Task<ActionResult<ResponseWrapper<IDictionary<string, object>, MetaData>>> Get(Guid id, string fields, [FromHeader(Name = "Accept")] string mediaType)
		{
			if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
			{
				return BadRequest("Invalid media type");
			}

			if (!_propertyCheckerService.TypeHasProperties<TestDTO>(fields))
			{
				return BadRequest("Fields are invalid");
			}

			var test = await _testBuilder.Build().GetById(id);
			if (test == null) return NotFound();

			var responseWrapper = CreateResponseWrapper(test, StatusCodes.Status200OK, fields, mediaType);
			return Ok(responseWrapper);
		}

		[HttpPost(Name = "CreateTest")]
		public async Task<ActionResult<ResponseWrapper<IEnumerable<IDictionary<string, object>>, MetaData>>> Insert([FromBody] TestInsertDTO model, string fields)
		{
			var test = await _testBuilder.Copy(model).Build().Insert();
			var responseWrapper = CreateResponseWrapper(test, StatusCodes.Status201Created, fields, null);

			return CreatedAtRoute("GetTest", new { Id = test.Id}, responseWrapper);
		}

		[HttpPut("{id:guid}", Name = "UpdateTest")]
		public async Task<ActionResult> Update (Guid id, [FromBody] TestUpdateDTO dto, [FromQuery] bool upsert)
		{
			var testDTO = await _testBuilder.Copy(dto).SetId(id).Build().Update();
			if (testDTO == null)
			{
				if (upsert)
				{
					testDTO = await _testBuilder.Copy(dto).SetId(id).Build().Insert();
					return CreatedAtRoute("GetTest", new { Id = testDTO.Id }, testDTO);
				}
				else
				{
					return NotFound();
				}
			}
			return NoContent();
		}

		[HttpDelete("{id:guid}", Name = "DeleteTest")]
		public async Task<ActionResult> Delete(Guid id)
		{
			var testDTO = await _testBuilder.SetId(id).Build().Delete();
			if (testDTO == null) return NotFound();

			return NoContent();
		}

		[HttpOptions]
		public ActionResult GetOptions()
		{
			Response.Headers.Add("Allow","GET,OPTIONS,POST,HEAD,PUT,DELETE");
			return Ok();
		}

		private string CreateTestResourceUri(TestQueryParams queryParams, ResourceUriType type)
		{
			switch (type)
			{
				case ResourceUriType.PreviousPage:
					return Url.Link("GetTestList",
						new
						{
							pageNumber = queryParams.PageNumber - 1,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
				case ResourceUriType.NextPage:
					return Url.Link("GetTestList",
						new
						{
							pageNumber = queryParams.PageNumber + 1,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
				case ResourceUriType.Current:
				default:
					return Url.Link("GetTestList",
						new
						{
							pageNumber = queryParams.PageNumber,
							pageSize = queryParams.PageSize,
							searchQuery = queryParams.SearchQuery,
							fields = queryParams.Fields
						});
			}
		}
		private ResponseWrapper<IEnumerable<IDictionary<string, object>>, ListMetaData > CreateResponseWrapper(PagedList<TestDTO> list, TestQueryParams query)
		{
			var previousPageLink = list.HasPrevious ?
				CreateTestResourceUri(query, ResourceUriType.PreviousPage) : null;

			var nextPageLink = list.HasNext ?
				CreateTestResourceUri(query, ResourceUriType.NextPage) : null;

			var listMetaData = new ListMetaData
			{
				TotalCount = list.TotalCount,
				PageSize = list.PageSize,
				CurrentPage = list.CurrentPage,
				TotalPages = list.TotalPages,
				OrderBy = query.OrderBy,
				SearchQuery = query.SearchQuery,
				Links = CreateLinksForTestList (query, list.HasNext, list.HasPrevious)
			};

			var shapedTest = ((IEnumerable<TestDTO>) list).ShapeData(query.Fields).Select(test =>
				{
					var testAsDictionary = test as IDictionary<string, object>;
					var testLinks = CreateLinksForTest((Guid) testAsDictionary["Id"], null);
					testAsDictionary.Add("links", testLinks);
					return testAsDictionary;
				});

			var responseWrapper = new ResponseWrapper<IEnumerable<IDictionary<string, object>>, ListMetaData >
			{
				results = shapedTest,
				MetaData = listMetaData
			};
			Response.Headers.Add("X-Paginaton",JsonSerializer.Serialize(listMetaData));
			return responseWrapper;
		}
		private ResponseWrapper<IDictionary<string, object>, MetaData> CreateResponseWrapper(TestDTO test, int statusCode, string fields, string mediaType)
		{
			var metaData = new MetaData
			{
				Message = "Successfully created",
				StatusCode = statusCode
			};

			var responseWrapper = new ResponseWrapper<IDictionary<string, object>, MetaData>
				{
					results = test.ShapeData(fields) as IDictionary<string, object>,
					MetaData = metaData
				};

			if (mediaType == "application/vnd.learnex.hateoas+json")
			{
				responseWrapper.results.Add("links", CreateLinksForTest(test.Id, fields));
			}
			return responseWrapper;
		}
		private IEnumerable<LinkDTO> CreateLinksForTest(Guid id, string fields)
		{
			var links = new List<LinkDTO>();
			if (string.IsNullOrWhiteSpace(fields))
			{
				links.Add(new LinkDTO(Url.Link("GetTest", new { id }), "self","GET"));
			}
			else
			{
				links.Add(new LinkDTO(Url.Link("GetTest", new { id, fields }), "update_test", "GET"));
			}

			links.Add(new LinkDTO(Url.Link("DeleteTest", new { id }), "delete_test", "DELETE"));
			links.Add(new LinkDTO(Url.Link("UpdateTest", new { id }), "update_test", "PUT"));
			return links;
		}
		private IEnumerable<LinkDTO> CreateLinksForTestList(TestQueryParams queryParams, bool hasNext, bool hasPrevious)
		{
			var links = new List<LinkDTO>();
			links.Add(new LinkDTO(CreateTestResourceUri(queryParams, ResourceUriType.Current), "self", "GET"));

			if (hasNext)
			{
				links.Add(new LinkDTO(CreateTestResourceUri(queryParams, ResourceUriType.NextPage), "nextPage", "GET"));
			}

			if (hasPrevious)
			{
				links.Add(new LinkDTO(CreateTestResourceUri(queryParams, ResourceUriType.PreviousPage), "previousPage", "GET"));
			}
			return links;
		}
	}
}