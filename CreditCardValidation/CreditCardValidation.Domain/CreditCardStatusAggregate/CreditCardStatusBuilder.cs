using System;
using AutoMapper;
using SharedKernel.Interfaces;
using CreditCardValidation.Common.Models.CreditCardStatuses;
namespace CreditCardValidation.Domain.CreditCardStatusAggregate
{
	public class CreditCardStatusBuilder : IBuild<CreditCardStatusBuilder, CreditCardStatus>
	{
		private readonly ICreditCardStatusUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public IMapper Mapper { get { return _mapper; } }
		public ICreditCardStatusUnitOfWork UnitOfWork { get { return _unitOfWork; } }

		public Guid Id { get; private set; }
		public string Status { get; private set; }
		public string Description { get; private set; }

		public CreditCardStatusBuilder(ICreditCardStatusUnitOfWork unitOfWork, IMapper mapper)		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public CreditCardStatusBuilder Copy(CreditCardStatus creditCardStatus)
		{
			Id = creditCardStatus.Id;
			Status = creditCardStatus.Status;
			Description = creditCardStatus.Description;
			return this;
		}

		public CreditCardStatusBuilder Copy(CreditCardStatusDTO creditCardStatusDTO)
		{
			Id = creditCardStatusDTO.Id;
			Status = creditCardStatusDTO.Status;
			Description = creditCardStatusDTO.Description;
			return this;
		}
		public CreditCardStatusBuilder Copy(CreditCardStatusInsertDTO creditCardStatusInsertDTO)
		{
			Status = creditCardStatusInsertDTO.Status;
			Description = creditCardStatusInsertDTO.Description;
			return this;
		}
		public CreditCardStatusBuilder Copy(CreditCardStatusUpdateDTO creditCardStatusUpdateDTO)
		{
			Status = creditCardStatusUpdateDTO.Status;
			Description = creditCardStatusUpdateDTO.Description;
			return this;
		}

		public CreditCardStatus Build()
		{
			return new CreditCardStatus(this);
		}

		public CreditCardStatusBuilder SetId (Guid id)
		{
			Id = id;
			return this;
		}
		public CreditCardStatusBuilder SetStatus (string status)
		{
			Status = status;
			return this;
		}
		public CreditCardStatusBuilder SetDescription (string description)
		{
			Description = description;
			return this;
		}
	}
}