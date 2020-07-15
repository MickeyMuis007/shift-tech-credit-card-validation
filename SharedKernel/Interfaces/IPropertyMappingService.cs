using System.Collections.Generic;
using SharedKernel.Models;

namespace SharedKernel.Interfaces
{
  public interface IPropertyMappingService
  {
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestionation>();
    bool ValidMappingExistsFor<TSource, TDestination>(string fields);
  }
}