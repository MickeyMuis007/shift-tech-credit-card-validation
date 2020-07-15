using System;
using SharedKernel.Interfaces;

namespace CreditCardValidation.Domain.TestAggregate
{
	public interface ITestRepository : IRepository<Test, Guid, TestQueryParams>
	{
	}
}