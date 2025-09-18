namespace ct_backend.Features.Brands
{
    public class QueryBrandRequest : AbstractRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Desc { get; set; }
    }
}