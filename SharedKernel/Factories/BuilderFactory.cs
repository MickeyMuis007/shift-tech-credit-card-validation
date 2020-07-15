using System;
using SharedKernel.Interfaces;

namespace SharedKernel.Factories {
  public class BuilderFactory <TBuilder> where TBuilder : class, IBuilder {
    public static TBuilder Create() {
      return (TBuilder)Activator.CreateInstance(typeof(TBuilder));
    }
  }
}