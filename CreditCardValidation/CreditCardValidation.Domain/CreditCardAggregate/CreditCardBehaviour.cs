using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Models;
using CreditCardValidation.Common.Models.CreditCards;

namespace CreditCardValidation.Domain.CreditCardAggregate
{
	public class CreditCardBehaviour : Entity<Guid>
	{
		private ICreditCardUnitOfWork _unitOfWork;
		private IMapper _mapper;
		private CreditCard _creditCard;

		protected CreditCardBehaviour() { }

		protected void BehaviourInit(CreditCard creditCard, CreditCardBuilder builder)		{
			_creditCard = creditCard;
			_unitOfWork = builder.UnitOfWork;
			_mapper = builder.Mapper;
		}
		public async Task<PagedList<CreditCardDTO>> GetAll(CreditCardQueryParams queryParams)		{
			var creditCardList = await _unitOfWork.CreditCardRepository.GetAll(queryParams);
			var creditCardDTOList = _mapper.Map<List<CreditCardDTO>>(creditCardList);
			var  pagedList = PagedList<CreditCardDTO>.Create(creditCardDTOList, creditCardList.TotalCount, creditCardList.CurrentPage, creditCardList.PageSize);
			return pagedList;
		}

		public IEnumerable<CreditCardDTO> Get5CreditCardsToProcess()
		{
			var creditCards = _unitOfWork.CreditCardRepository.Get5CreditCardsToProcess();
			var dtos = _mapper.Map<IEnumerable<CreditCardDTO>>(creditCards);
			return dtos;
		}
		public async Task<CreditCardDTO> GetById(Guid id)
		{
			var creditCard = await _unitOfWork.CreditCardRepository.GetById(id);
			var creditCardDTO = _mapper.Map<CreditCardDTO>(creditCard);
			return creditCardDTO;
		}
		public async Task<CreditCardDTO> Insert()
		{
			var creditCard = await _unitOfWork.CreditCardRepository.Insert(_creditCard);
			await _unitOfWork.SaveAsync();
			var creditCardDTO = _mapper.Map<CreditCardDTO>(creditCard);
			return creditCardDTO;
		}
		public async Task<CreditCardDTO> Update()
		{
			var exists = _unitOfWork.CreditCardRepository.Exists(Id);
			if (!exists) return null;

			_unitOfWork.CreditCardRepository.Update(_creditCard);
			await _unitOfWork.SaveAsync();
			var creditCardDTO = _mapper.Map<CreditCardDTO>(_creditCard);
			return creditCardDTO;
		}
		public async Task<CreditCardDTO> Delete()
		{
			var creditCard = await _unitOfWork.CreditCardRepository.GetById(Id);
			if (creditCard == null) return null;

			_unitOfWork.CreditCardRepository.Delete(creditCard);
			await _unitOfWork.SaveAsync();
			var creditCardDTO = _mapper.Map<CreditCardDTO>(creditCard);
			return creditCardDTO;
		}
	}
}