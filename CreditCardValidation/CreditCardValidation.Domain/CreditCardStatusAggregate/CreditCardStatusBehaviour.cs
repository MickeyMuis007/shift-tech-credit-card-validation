using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Models;
using CreditCardValidation.Common.Models.CreditCardStatuses;

namespace CreditCardValidation.Domain.CreditCardStatusAggregate
{
	public class CreditCardStatusBehaviour : Entity<Guid>
	{
		private ICreditCardStatusUnitOfWork _unitOfWork;
		private IMapper _mapper;
		private CreditCardStatus _creditCardStatus;

		protected CreditCardStatusBehaviour() { }

		protected void BehaviourInit(CreditCardStatus creditCardStatus, CreditCardStatusBuilder builder)		{
			_creditCardStatus = creditCardStatus;
			_unitOfWork = builder.UnitOfWork;
			_mapper = builder.Mapper;
		}
		public async Task<PagedList<CreditCardStatusDTO>> GetAll(CreditCardStatusQueryParams queryParams)		{
			var creditCardStatusList = await _unitOfWork.CreditCardStatusRepository.GetAll(queryParams);
			var creditCardStatusDTOList = _mapper.Map<List<CreditCardStatusDTO>>(creditCardStatusList);
			var  pagedList = PagedList<CreditCardStatusDTO>.Create(creditCardStatusDTOList, creditCardStatusList.TotalCount, creditCardStatusList.CurrentPage, creditCardStatusList.PageSize);
			return pagedList;
		}
		public async Task<CreditCardStatusDTO> GetById(Guid id)
		{
			var creditCardStatus = await _unitOfWork.CreditCardStatusRepository.GetById(id);
			var creditCardStatusDTO = _mapper.Map<CreditCardStatusDTO>(creditCardStatus);
			return creditCardStatusDTO;
		}
		public async Task<CreditCardStatusDTO> Insert()
		{
			var creditCardStatus = await _unitOfWork.CreditCardStatusRepository.Insert(_creditCardStatus);
			await _unitOfWork.SaveAsync();
			var creditCardStatusDTO = _mapper.Map<CreditCardStatusDTO>(creditCardStatus);
			return creditCardStatusDTO;
		}
		public async Task<CreditCardStatusDTO> Update()
		{
			var creditCardStatus = await _unitOfWork.CreditCardStatusRepository.GetById(Id);
			if (creditCardStatus == null) return null;

			_mapper.Map(_creditCardStatus, creditCardStatus);
			await _unitOfWork.SaveAsync();
			var creditCardStatusDTO = _mapper.Map<CreditCardStatusDTO>(creditCardStatus);
			return creditCardStatusDTO;
		}
		public async Task<CreditCardStatusDTO> Delete()
		{
			var creditCardStatus = await _unitOfWork.CreditCardStatusRepository.GetById(Id);
			if (creditCardStatus == null) return null;

			_unitOfWork.CreditCardStatusRepository.Delete(creditCardStatus);
			await _unitOfWork.SaveAsync();
			var creditCardStatusDTO = _mapper.Map<CreditCardStatusDTO>(creditCardStatus);
			return creditCardStatusDTO;
		}
	}
}