using System;
using System.Collections.Generic;

namespace DCWR.Event_Manager.Contracts.Utilities
{
    public interface IPagedResponse
    {
        PagingInfo Paging { get; }
    }

    public class PagedResponse<T> : IPagedResponse where T : class
    {
        public IReadOnlyCollection<T> Results { get; }

        public PagingInfo Paging { get; }

        public PagedResponse(IReadOnlyCollection<T> results, PagingInfo paging)
        {
            Results = results ?? Array.Empty<T>();
            Paging = paging;
        }
    }
}
