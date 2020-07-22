using System;
using System.Linq;
using System.Threading.Tasks;
using CreditCardValidation.Domain.TestAggregate;
using CreditCardValidation.Persistence.Contexts;
using SharedKernel.Models;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.Tests
{
	public class TestRepository : Repository<Test, Guid, TestQueryParams> , ITestRepository
	{
		private IPropertyMappingService _propertyMappingService;

		public TestRepository (CreditCardValidationDBContexts db, IPropertyMappingService propertyMappingService) : base(db)
		{
			_propertyMappingService = propertyMappingService;
		}

		public new async Task<PagedList<Test>> GetAll(TestQueryParams queryParams)
		{
			var searchQuery = queryParams?.SearchQuery?.Trim();
			var testsQueryable = _db.Tests.Where(t =>
				t.Name.Contains(searchQuery ?? t.Name) ||
				t.LastName.Contains(searchQuery ?? t.LastName) ||
				t.PhoneNumber.Contains(searchQuery ?? t.PhoneNumber)
			);

			if (!string.IsNullOrWhiteSpace(queryParams.OrderBy))
			{
				if (queryParams.OrderBy.ToLowerInvariant() == "name")
				{
					testsQueryable = testsQueryable.OrderBy(t => t.Name)
						.ThenBy(t => t.LastName);
				}
			}

			var tests = await PagedList<Test>.Create(testsQueryable, queryParams.PageNumber, queryParams.PageSize);
			return tests;
			
		}
	}
}
