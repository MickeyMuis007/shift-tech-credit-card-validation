using System;
using AutoMapper;
using SharedKernel.Interfaces;
using CreditCardValidation.Common.Models.TestModels;
namespace CreditCardValidation.Domain.TestAggregate
{
	public class TestBuilder : IBuild<TestBuilder, Test>
	{
		private readonly ITestUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public IMapper Mapper { get { return _mapper; } }
		public ITestUnitOfWork UnitOfWork { get { return _unitOfWork; } }

		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public string LastName { get; private set; }
		public string PhoneNumber { get; private set; }
		public DateTime? BirthDate { get; private set; }

		public TestBuilder(ITestUnitOfWork unitOfWork, IMapper mapper)		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public TestBuilder Copy(Test test)
		{
			Id = test.Id;
			Name = test.Name;
			LastName = test.LastName;
			PhoneNumber = test.PhoneNumber;
			BirthDate = test.BirthDate;
			return this;
		}

		public TestBuilder Copy(TestDTO testDTO)
		{
			Id = testDTO.Id;
			Name = testDTO.Name;
			LastName = testDTO.LastName;
			PhoneNumber = testDTO.PhoneNumber;
			BirthDate = testDTO.BirthDate;
			return this;
		}
		public TestBuilder Copy(TestInsertDTO testInsertDTO)
		{
			Id = testInsertDTO.Id;
			Name = testInsertDTO.Name;
			LastName = testInsertDTO.LastName;
			PhoneNumber = testInsertDTO.PhoneNumber;
			BirthDate = testInsertDTO.BirthDate;
			return this;
		}
		public TestBuilder Copy(TestUpdateDTO testUpdateDTO)
		{
			Id = testUpdateDTO.Id;
			Name = testUpdateDTO.Name;
			LastName = testUpdateDTO.LastName;
			PhoneNumber = testUpdateDTO.PhoneNumber;
			BirthDate = testUpdateDTO.BirthDate;
			return this;
		}

		public Test Build()
		{
			return new Test(this);
		}

		public TestBuilder SetId (Guid id)
		{
			Id = id;
			return this;
		}
		public TestBuilder SetName (string name)
		{
			Name = name;
			return this;
		}
		public TestBuilder SetLastName (string lastName)
		{
			LastName = lastName;
			return this;
		}
		public TestBuilder SetPhoneNumber (string phoneNumber)
		{
			PhoneNumber = phoneNumber;
			return this;
		}
		public TestBuilder SetBirthDate (DateTime? birthDate)
		{
			BirthDate = birthDate;
			return this;
		}
	}
}