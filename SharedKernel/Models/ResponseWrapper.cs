namespace SharedKernel.Models {
  public class ResponseWrapper<T, TMetaData> {
    public TMetaData MetaData { get; set; }
    public T results { get; set; }
  }
}