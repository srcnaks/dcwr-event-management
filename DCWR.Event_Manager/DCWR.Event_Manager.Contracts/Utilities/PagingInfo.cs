using System;
using System.Collections.Generic;
using System.Text;

namespace DCWR.Event_Manager.Contracts.Utilities
{
    public class PagingInfo
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public PagingInfo(
            int pageNumber, 
            int pageSize, 
            int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
