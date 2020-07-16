using System;
using AutoMapper;
using SharedKernel.Interfaces;
using CreditCardValidation.Common.Models.CreditCardProviders;
namespace CreditCardValidation.Domain.CreditCardProviderAggregate
{
	public class CreditCardProviderBuilder : IBuild<CreditCardProviderBuilder, CreditCardProvider>
	{
		private readonly ICreditCardProviderUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public IMapper Mapper { get { return _mapper; } }
		public ICreditCardProviderUnitOfWork UnitOfWork { get { return _unitOfWork; } }

		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public string Code { get; private set; }
		public string StartsWith { get; private set; }
		public string Length { get; private set; }

		public CreditCardProviderBuilder(ICreditCardProviderUnitOfWork unitOfWork, IMapper mapper)		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public CreditCardProviderBuilder Copy(CreditCardProvider creditCardProvider)
		{
			Id = creditCardProvider.Id;
			Name = creditCardProvider.Name;
			Code = creditCardProvider.Code;
			StartsWith = creditCardProvider.StartsWith;
			Length = creditCardProvider.Length;
			return this;
		}

		public CreditCardProviderBuilder Copy(CreditCardProviderDTO creditCardProviderDTO)
		{
			Id = creditCardProviderDTO.Id;
			Name = creditCardProviderDTO.Name;
			Code = creditCardProviderDTO.Code;
			StartsWith = creditCardProviderDTO.StartsWith;
			Length = creditCardProviderDTO.Length;
			return this;
		}
		public CreditCardProviderBuilder Copy(CreditCardProviderInsertDTO creditCardProviderInsertDTO)
		{
			Name = creditCardProviderInsertDTO.Name;
			Code = creditCardProviderInsertDTO.Code;
			StartsWith = creditCardProviderInsertDTO.StartsWith;
			Length = creditCardProviderInsertDTO.Length;
			return this;
		}
		public CreditCardProviderBuilder Copy(CreditCardProviderUpdateDTO creditCardProviderUpdateDTO)
		{
			Name = creditCardProviderUpdateDTO.Name;
			Code = creditCardProviderUpdateDTO.Code;
			StartsWith = creditCardProviderUpdateDTO.StartsWith;
			Length = creditCardProviderUpdateDTO.Length;
			return this;
		}

		public CreditCardProvider Build()
		{
			return new CreditCardProvider(this);
		}

		public CreditCardProviderBuilder SetId (Guid id)
		{
			Id = id;
			return this;
		}
		public CreditCardProviderBuilder SetName (string name)
		{
			Name = name;
			return this;
		}
		public CreditCardProviderBuilder SetCode (string code)
		{
			Code = code;
			return this;
		}
		public CreditCardProviderBuilder SetStartsWith (string startsWith)
		{
			StartsWith = startsWith;
			return this;
		}
		public CreditCardProviderBuilder SetLength (string length)
		{
			Length = length;
			return this;
		}
	}
}