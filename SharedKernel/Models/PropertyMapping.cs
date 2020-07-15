using System.Collections.Generic;
using System;

using SharedKernel.Interfaces;

namespace SharedKernel.Models {
  public class PropertyMapping<TSource, TDestination> : IPropertyMapping {
    public Dictionary<string, PropertyMappingValue> _mappingDictionary { get; private set; }

    public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary) {
      _mappingDictionary = mappingDictionary ?? 
        throw new ArgumentException(nameof(mappingDictionary));
    }
  }
}