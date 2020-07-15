using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Models;
using CreditCardValidation.Common.Models.TestModels;

namespace CreditCardValidation.Domain.TestAggregate
{
	public class TestBehaviour : Entity<Guid>
	{
		private ITestUnitOfWork _unitOfWork;
		private IMapper _mapper;
		private Test _test;
		private ITestUnitOfWork _unitOfWork2 { get; }

		protected TestBehaviour() { }

		protected void BehaviourInit(Test test, TestBuilder builder)		{
			_test = test;
			_unitOfWork = builder.UnitOfWork;
			_mapper = builder.Mapper;
		}
		public async Task<PagedList<TestDTO>> GetAll(TestQueryParams queryParams)		{
			var testList = await _unitOfWork.TestRepository.GetAll(queryParams);
			var testDTOList = _mapper.Map<List<TestDTO>>(testList);
			var  pagedList = PagedList<TestDTO>.Create(testDTOList, testList.TotalCount, testList.CurrentPage, testList.PageSize);
			return pagedList;
		}
		public async Task<TestDTO> GetById(Guid id)
		{
			var test = await _unitOfWork.TestRepository.GetById(id);
			var testDTO = _mapper.Map<TestDTO>(test);
			return testDTO;
		}
		public async Task<TestDTO> Insert()
		{
			var test = await _unitOfWork.TestRepository.Insert(_test);
			await _unitOfWork.SaveAsync();
			var testDTO = _mapper.Map<TestDTO>(test);
			return testDTO;
		}
		public async Task<TestDTO> Update()
		{
			var test = await _unitOfWork.TestRepository.GetById(Id);
			if (test == null) return null;

			_mapper.Map(_test, test);
			await _unitOfWork.SaveAsync();
			var testDTO = _mapper.Map<TestDTO>(test);
			return testDTO;
		}
		public async Task<TestDTO> Delete()
		{
			var test = await _unitOfWork.TestRepository.GetById(Id);
			if (test == null) return null;

			_unitOfWork.TestRepository.Delete(test);
			await _unitOfWork.SaveAsync();
			var testDTO = _mapper.Map<TestDTO>(test);
			return testDTO;
		}
	}
}