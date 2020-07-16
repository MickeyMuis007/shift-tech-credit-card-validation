using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
				t.No.Contains(searchQuery ?? t.No) &&
				t.CreditCardStatusId == (queryParams.CreditCardStatusId ?? t.CreditCardStatusId)
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

		public IEnumerable<CreditCard> Get5CreditCardsToProcess()
		{
			var issuedStatusId = _db.CreditCardStatuses.FirstOrDefault(t => t.Status == "Issued")?.Id;
			var processedStatus = _db.CreditCardStatuses.FirstOrDefault(t => t.Status == "Processed");

			var creditCards = _db.CreditCards.Where(t => t.CreditCardStatusId == issuedStatusId).Take(5);

			var builder = new CreditCardBuilder(null, null);
			var list = new List<CreditCard>();

			foreach (CreditCard creditCard in creditCards)
			{
				_db.Entry(creditCard).State = EntityState.Detached;
				if (processedStatus != null) {
					list.Add(builder.Copy(creditCard).SetCreditCardStatusId(processedStatus.Id).Build());
				}
			}

			return list;
		}

		public bool Exists(Guid id)
		{
			return _db.CreditCards.Any(t => t.Id == id);
		}
	}
}
