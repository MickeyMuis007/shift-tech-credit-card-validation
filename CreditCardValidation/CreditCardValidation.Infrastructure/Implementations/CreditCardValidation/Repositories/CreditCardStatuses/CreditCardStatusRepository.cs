using System;
using System.Linq;
using System.Threading.Tasks;
using CreditCardValidation.Domain.CreditCardStatusAggregate;
using CreditCardValidation.Persistence.Contexts;
using CreditCardValidation.Common.Models.CreditCardStatuses;
using SharedKernel.Models;
using SharedKernel.Helpers;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCardStatuses
{
	public class CreditCardStatusRepository : Repository<CreditCardStatus, Guid, CreditCardStatusQueryParams> , ICreditCardStatusRepository
	{
		private IPropertyMappingService _propertyMappingService;

		public CreditCardStatusRepository (CreditCardValidationDBContexts db, IPropertyMappingService propertyMappingService) : base(db)
		{
			_propertyMappingService = propertyMappingService;
		}

		public new async Task<PagedList<CreditCardStatus>> GetAll(CreditCardStatusQueryParams queryParams)
		{
			var searchQuery = queryParams?.SearchQuery?.Trim();
			var creditCardStatusesQueryable = _db.CreditCardStatuses.Where(t =>
				t.Status.Contains(searchQuery ?? t.Status) ||
				t.Description.Contains(searchQuery ?? t.Description)
			);

			if (!string.IsNullOrWhiteSpace(queryParams.OrderBy))
			{
				if (queryParams.OrderBy.ToLowerInvariant() == "status")
				{
					creditCardStatusesQueryable = creditCardStatusesQueryable.OrderBy(t => t.Status)
						.ThenBy(t => t.Description);
				}
				var creditCardStatusPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<CreditCardStatusDTO, CreditCardStatus>();
				creditCardStatusesQueryable = creditCardStatusesQueryable.ApplySort(queryParams.OrderBy, creditCardStatusPropertyMappingDictionary);;
			}

			var creditCardStatuses = await PagedList<CreditCardStatus>.Create(creditCardStatusesQueryable, queryParams.PageNumber, queryParams.PageSize);
			return creditCardStatuses;
			
		}

		public bool Exists(Guid id)
		{
			return _db.CreditCardStatuses.Any(t => t.Id == id);
		}
	}
}
