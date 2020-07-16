using System;
using System.Linq;
using System.Collections.Generic;
using SharedKernel.Models;
using SharedKernel.Interfaces;
using CreditCardValidation.Common.Models.TestModels;
using CreditCardValidation.Common.Models.CreditCardStatuses;
using CreditCardValidation.Common.Models.CreditCardProviders;
using CreditCardValidation.Domain.TestAggregate;
using CreditCardValidation.Domain.CreditCardStatusAggregate;
using CreditCardValidation.Domain.CreditCardProviderAggregate;

namespace Application.Services
{
  public class PropertyMappingService : IPropertyMappingService
  {
    private IList<IPropertyMapping> _propertyMappings;
    private Dictionary<string, PropertyMappingValue> _teacherPropertyMapping =
      new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
      {
        { "Id", new PropertyMappingValue(new List<string>() { "Id" })},
        { "Code", new PropertyMappingValue(new List<string>() { "Code"})},
        { "Name", new PropertyMappingValue(new List<string>() { "Name"})},
        { "LastName", new PropertyMappingValue(new List<string>() { "LastName"})},
        { "PhoneNumber", new PropertyMappingValue(new List<string>() {"PhoneNumber"})},
        { "Status", new PropertyMappingValue(new List<string>() {"Status"})},
        { "Description", new PropertyMappingValue(new List<string>() {"Description"})},
        { "No", new PropertyMappingValue(new List<string>() {"No"})},
        { "TestNames", new PropertyMappingValue(new List<string>() { "Name", "LastName"})},
        { "TestAge", new PropertyMappingValue(new List<string>() { "BirthDate"}, revert: true)},
        { "TestPhoneNumber", new PropertyMappingValue(new List<string>() {"PhoneNumber"})},
        { "CreditCardStatusNames", new PropertyMappingValue(new List<string>() {"Status", "Description"})},
        { "CreditCardProviderNames", new PropertyMappingValue(new List<string>() { "Name", "Code"})},
        { "CreditCardProviderCode", new PropertyMappingValue(new List<string>() {"Code"})}
      };

    public PropertyMappingService() {
      _propertyMappings = new List<IPropertyMapping>();
      _propertyMappings.Add(new PropertyMapping<TestDTO, Test>(_teacherPropertyMapping));
      _propertyMappings.Add(new PropertyMapping<CreditCardStatusDTO, CreditCardStatus>(_teacherPropertyMapping));
      _propertyMappings.Add(new PropertyMapping<CreditCardProviderDTO, CreditCardProvider>(_teacherPropertyMapping));
    }


    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>() 
    {
      // get matching mapping
      var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

      if (matchingMapping.Count() == 1) {
        return matchingMapping.First()._mappingDictionary;
      }

      throw new Exception($"Cannot find exact property mapping instantce " + $"for <{typeof(TSource)},{typeof(TDestination)}");
    }

    public bool ValidMappingExistsFor<TSource, TDestination>(string fields) {
      var propertyMapping = GetPropertyMapping<TSource, TDestination>();

      if (string.IsNullOrWhiteSpace(fields)) {
        return true;
      }

      // the string is separated by "," so we split it.
      var fieldsAfterSplit = fields.Split(',');

      // run through the fields clauses
      foreach (var field in fieldsAfterSplit) {
        // trim
        var trimmedField = field.Trim();

        // remove everything after the first " " - if the fields
        // are coming from an orderBy string, this part must be ignored
        var indexOfFirstSpace = trimmedField.IndexOf(" ");
        var propertyName = indexOfFirstSpace == -1 ?
          trimmedField : trimmedField.Remove(indexOfFirstSpace);

        // find the matching property
        if (!propertyMapping.ContainsKey(propertyName)) {
          return false;
        }

      }
      
      return true;
    }
  }
}