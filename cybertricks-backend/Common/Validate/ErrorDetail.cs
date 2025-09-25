namespace ct.backend.Common.Validate
{
    public class ErrorDetail
    {
        public string Code { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string Field { get; set; } = "General";
    }
}
