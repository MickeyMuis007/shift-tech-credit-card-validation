using System;
using System.Linq;
using System.Threading.Tasks;
using CreditCardValidation.Domain.CreditCardProviderAggregate;
using CreditCardValidation.Persistence.Contexts;
using CreditCardValidation.Common.Models.CreditCardProviders;
using SharedKernel.Models;
using SharedKernel.Helpers;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCardProviders
{
	public class CreditCardProviderRepository : Repository<CreditCardProvider, Guid, CreditCardProviderQueryParams> , ICreditCardProviderRepository
	{
		private IPropertyMappingService _propertyMappingService;

		public CreditCardProviderRepository (CreditCardValidationDBContexts db, IPropertyMappingService propertyMappingService) : base(db)
		{
			_propertyMappingService = propertyMappingService;
		}

		public new async Task<PagedList<CreditCardProvider>> GetAll(CreditCardProviderQueryParams queryParams)
		{
			var searchQuery = queryParams?.SearchQuery?.Trim();
			var creditCardProvidersQueryable = _db.CreditCardProviders.Where(t =>
				t.Name.Contains(searchQuery ?? t.Name) ||
				t.Code.Contains(searchQuery ?? t.Code)
			);

			if (!string.IsNullOrWhiteSpace(queryParams.OrderBy))
			{
				if (queryParams.OrderBy.ToLowerInvariant() == "name")
				{
					creditCardProvidersQueryable = creditCardProvidersQueryable.OrderBy(t => t.Name)
						.ThenBy(t => t.Code);
				}
				var creditCardProviderPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<CreditCardProviderDTO, CreditCardProvider>();
				creditCardProvidersQueryable = creditCardProvidersQueryable.ApplySort(queryParams.OrderBy, creditCardProviderPropertyMappingDictionary);;
			}

			var creditCardProviders = await PagedList<CreditCardProvider>.Create(creditCardProvidersQueryable, queryParams.PageNumber, queryParams.PageSize);
			return creditCardProviders;
			
		}

		public bool Exists(Guid id)
		{
			return _db.CreditCardProviders.Any(t => t.Id == id);
		}
	}
}
