namespace DCWR.Event_Manager.WebApi.Models
{
    public class PagedRequest
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
