using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Models;
using CreditCardValidation.Common.Models.CreditCardProviders;

namespace CreditCardValidation.Domain.CreditCardProviderAggregate
{
	public class CreditCardProviderBehaviour : Entity<Guid>
	{
		private ICreditCardProviderUnitOfWork _unitOfWork;
		private IMapper _mapper;
		private CreditCardProvider _creditCardProvider;

		protected CreditCardProviderBehaviour() { }

		protected void BehaviourInit(CreditCardProvider creditCardProvider, CreditCardProviderBuilder builder)		{
			_creditCardProvider = creditCardProvider;
			_unitOfWork = builder.UnitOfWork;
			_mapper = builder.Mapper;
		}
		public async Task<PagedList<CreditCardProviderDTO>> GetAll(CreditCardProviderQueryParams queryParams)		{
			var creditCardProviderList = await _unitOfWork.CreditCardProviderRepository.GetAll(queryParams);
			var creditCardProviderDTOList = _mapper.Map<List<CreditCardProviderDTO>>(creditCardProviderList);
			var  pagedList = PagedList<CreditCardProviderDTO>.Create(creditCardProviderDTOList, creditCardProviderList.TotalCount, creditCardProviderList.CurrentPage, creditCardProviderList.PageSize);
			return pagedList;
		}
		public async Task<CreditCardProviderDTO> GetById(Guid id)
		{
			var creditCardProvider = await _unitOfWork.CreditCardProviderRepository.GetById(id);
			var creditCardProviderDTO = _mapper.Map<CreditCardProviderDTO>(creditCardProvider);
			return creditCardProviderDTO;
		}
		public async Task<CreditCardProviderDTO> Insert()
		{
			var creditCardProvider = await _unitOfWork.CreditCardProviderRepository.Insert(_creditCardProvider);
			await _unitOfWork.SaveAsync();
			var creditCardProviderDTO = _mapper.Map<CreditCardProviderDTO>(creditCardProvider);
			return creditCardProviderDTO;
		}
		public async Task<CreditCardProviderDTO> Update()
		{
			var exists = _unitOfWork.CreditCardProviderRepository.Exists(Id);
			if (!exists) return null;

			_unitOfWork.CreditCardProviderRepository.Update(_creditCardProvider);
			await _unitOfWork.SaveAsync();
			var creditCardProviderDTO = _mapper.Map<CreditCardProviderDTO>(_creditCardProvider);
			return creditCardProviderDTO;
		}
		public async Task<CreditCardProviderDTO> Delete()
		{
			var creditCardProvider = await _unitOfWork.CreditCardProviderRepository.GetById(Id);
			if (creditCardProvider == null) return null;

			_unitOfWork.CreditCardProviderRepository.Delete(creditCardProvider);
			await _unitOfWork.SaveAsync();
			var creditCardProviderDTO = _mapper.Map<CreditCardProviderDTO>(creditCardProvider);
			return creditCardProviderDTO;
		}
	}
}