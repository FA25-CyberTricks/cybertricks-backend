namespace ct_backend.Features
{
    public abstract class AbstractRequest
    {
        /// <summary>
        /// Id tương quan để trace log giữa các service (tuỳ chọn).
        /// </summary>
        public virtual string? CorrelationId { get; init; }


        /// <summary>
        /// Thời điểm client tạo/gửi request (UTC) – tuỳ chọn.
        /// </summary>
        public virtual DateTimeOffset RequestedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}
