using System;
using AutoMapper;
using SharedKernel.Interfaces;
using CreditCardValidation.Common.Models.CreditCards;using CreditCardValidation.Domain.CreditCardStatusAggregate;
using CreditCardValidation.Domain.CreditCardProviderAggregate;

namespace CreditCardValidation.Domain.CreditCardAggregate
{
	public class CreditCardBuilder : IBuild<CreditCardBuilder, CreditCard>
	{
		private readonly ICreditCardUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public IMapper Mapper { get { return _mapper; } }
		public ICreditCardUnitOfWork UnitOfWork { get { return _unitOfWork; } }

		public Guid Id { get; private set; }
		public string No { get; private set; }
		public Guid CreditCardStatusId { get; private set; }
		public Guid CreditCardProviderId { get; private set; }
		public CreditCardStatus CreditCardStatus { get; private set; }
		public CreditCardProvider CreditCardProvider { get; private set; }

		public CreditCardBuilder(ICreditCardUnitOfWork unitOfWork, IMapper mapper)		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public CreditCardBuilder Copy(CreditCard creditCard)
		{
			Id = creditCard.Id;
			No = creditCard.No;
			CreditCardStatusId = creditCard.CreditCardStatusId;
			CreditCardProviderId = creditCard.CreditCardProviderId;
			CreditCardStatus = creditCard.CreditCardStatus;
			CreditCardProvider = creditCard.CreditCardProvider;
			return this;
		}

		public CreditCardBuilder Copy(CreditCardDTO creditCardDTO)
		{
			Id = creditCardDTO.Id;
			No = creditCardDTO.No;
			CreditCardStatusId = creditCardDTO.CreditCardStatusId;
			CreditCardProviderId = creditCardDTO.CreditCardProviderId;
			return this;
		}
		public CreditCardBuilder Copy(CreditCardInsertDTO creditCardInsertDTO)
		{
			No = creditCardInsertDTO.No;
			CreditCardStatusId = creditCardInsertDTO.CreditCardStatusId;
			CreditCardProviderId = creditCardInsertDTO.CreditCardProviderId;
			return this;
		}
		public CreditCardBuilder Copy(CreditCardUpdateDTO creditCardUpdateDTO)
		{
			No = creditCardUpdateDTO.No;
			CreditCardStatusId = creditCardUpdateDTO.CreditCardStatusId;
			CreditCardProviderId = creditCardUpdateDTO.CreditCardProviderId;
			return this;
		}

		public CreditCard Build()
		{
			return new CreditCard(this);
		}

		public CreditCardBuilder SetId (Guid id)
		{
			Id = id;
			return this;
		}
		public CreditCardBuilder SetNo (string no)
		{
			No = no;
			return this;
		}
		public CreditCardBuilder SetCreditCardStatusId (Guid creditCardStatusId)
		{
			CreditCardStatusId = creditCardStatusId;
			return this;
		}
		public CreditCardBuilder SetCreditCardProviderId (Guid creditCardProviderId)
		{
			CreditCardProviderId = creditCardProviderId;
			return this;
		}
		public CreditCardBuilder SetCreditCardStatus (CreditCardStatus creditCardStatus)
		{
			CreditCardStatus = creditCardStatus;
			return this;
		}
		public CreditCardBuilder SetCreditCardProvider (CreditCardProvider creditCardProvider)
		{
			CreditCardProvider = creditCardProvider;
			return this;
		}
	}
}