namespace DCWR.Event_Manager.Contracts.Utilities
{
    public interface IQuery<out TResponse>
    {
        
    }

    public interface IPagedQuery<out TPagedResponse> : IQuery<TPagedResponse>
    where TPagedResponse : IPagedResponse
    {
        public int PageSize { get; }

        public int PageNumber { get; }
    }
}