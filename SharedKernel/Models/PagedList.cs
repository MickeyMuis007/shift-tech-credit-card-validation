using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Models {
  public class PagedList<T> : List<T> {
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => (CurrentPage > 1 && CurrentPage < TotalPages);
    public bool HasNext => (CurrentPage < TotalPages);

    private PagedList(List<T> items, int count, int pageNumber, int pageSize) {
      TotalCount = count;
      PageSize = pageSize;
      CurrentPage = pageNumber;
      TotalPages = (int)Math.Ceiling(count / (double)pageSize);
      AddRange(items);
    }

    public static async Task<PagedList<T>> Create(IQueryable<T> source, int pageNumber, int pageSize) {
      var count = source.Count();
      pageNumber = pageNumber > 1 ? pageNumber : 1;
      var items = await source.Skip((pageNumber -1) * pageSize).Take(pageSize).ToListAsync();
      return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static PagedList<T> Create(List<T> source, int totalCount, int pageNumber, int pageSize) {
      return new PagedList<T>(source, totalCount, pageNumber, pageSize);
    }
  }
}