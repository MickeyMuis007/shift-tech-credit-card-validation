namespace SharedKernel.Models
{
  public class QueryParams
  {
    const int MAX_PAGE_SIZE = 20;
    private int _pageSize = 10;

    /// <summary>
    /// This is the search query
    /// </summary>
    /// <value></value>
    public string SearchQuery { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
      get => _pageSize;
      set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
    }
    public string OrderBy { get; set; }
    public string Fields { get; set; }
  }
}