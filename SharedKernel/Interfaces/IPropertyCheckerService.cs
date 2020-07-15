namespace SharedKernel.Interfaces
{
  public interface IPropertyCheckerService {
    bool TypeHasProperties<T>(string fields);
  }
}