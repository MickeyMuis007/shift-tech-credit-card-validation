using System.Collections.Generic;
namespace SharedKernel.Models {
  public class ListMetaData: MetaData {
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string OrderBy { get; set; }
    public string SearchQuery { get; set; }
    public IEnumerable<LinkDTO> Links { get; set; }
  }
}