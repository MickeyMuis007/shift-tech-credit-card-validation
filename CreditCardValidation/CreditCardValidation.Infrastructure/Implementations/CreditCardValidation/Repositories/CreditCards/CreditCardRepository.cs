using System;
using System.Linq;
using System.Threading.Tasks;
using CreditCardValidation.Domain.CreditCardAggregate;
using CreditCardValidation.Persistence.Contexts;
using CreditCardValidation.Common.Models.CreditCards;
using SharedKernel.Models;
using SharedKernel.Helpers;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Infrastructure.Implementations.CreditCardValidation.Repositories.CreditCards
{
	public class CreditCardRepository : Repository<CreditCard, Guid, CreditCardQueryParams> , ICreditCardRepository
	{
		private IPropertyMappingService _propertyMappingService;

		public CreditCardRepository (CreditCardValidationDBContexts db, IPropertyMappingService propertyMappingService) : base(db)
		{
			_propertyMappingService = propertyMappingService;
		}

		public new async Task<PagedList<CreditCard>> GetAll(CreditCardQueryParams queryParams)
		{
			var searchQuery = queryParams?.SearchQuery?.Trim();
			var creditCardsQueryable = _db.CreditCards.Where(t =>
				t.No.Contains(searchQuery ?? t.No)
			);

			if (!string.IsNullOrWhiteSpace(queryParams.OrderBy))
			{
				if (queryParams.OrderBy.ToLowerInvariant() == "no")
				{
					creditCardsQueryable = creditCardsQueryable.OrderBy(t => t.No);
				}
				var creditCardPropertyMappingDictionary = _propertyMappingService.GetPropertyMapping<CreditCardDTO, CreditCard>();
				creditCardsQueryable = creditCardsQueryable.ApplySort(queryParams.OrderBy, creditCardPropertyMappingDictionary);;
			}

			var creditCards = await PagedList<CreditCard>.Create(creditCardsQueryable, queryParams.PageNumber, queryParams.PageSize);
			return creditCards;
			
		}

		public bool Exists(Guid id)
		{
			return _db.CreditCards.Any(t => t.Id == id);
		}
	}
}
