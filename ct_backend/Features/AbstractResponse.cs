using ct_backend.Common.Validate;

namespace ct_backend.Features
{
    public abstract class AbstractResponse<T>
    {
        public bool Success { get; set; } = true;
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<ErrorDetail> Errors { get; } = new();
    }
}
