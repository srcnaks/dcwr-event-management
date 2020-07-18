namespace DCWR.Event_Manager.WebApi.Models
{
    public class PagedRequest
    {
        public int PageSize { get; }
        public int PageNumber { get; }

        public PagedRequest(int pageSize = 10, int pageNumber = 1)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
