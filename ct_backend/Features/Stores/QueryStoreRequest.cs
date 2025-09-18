namespace ct_backend.Features.Stores
{
    public class QueryStoreRequest : AbstractRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
    }
}