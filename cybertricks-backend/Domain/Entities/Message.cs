using ct.backend.Domain.Enum;

namespace ct.backend.Domain.Entities
{
    public class Message : BaseEntity
    {
        public int MessageId { get; set; }
        public string Content { get; set; } = default!;

        public int? StoreId { get; set; }
        public virtual Store? Store { get; set; }

        public string FromUserId { get; set; } = default!;
        public virtual User FromUser { get; set; } = default!;

        public string? ToUserId { get; set; }
        public virtual User? ToUser { get; set; }

        public UserRole? ToRole { get; set; } // dùng UserRole cho tiện
        public MessageType Type { get; set; } = MessageType.text;
        public MessageStatus Status { get; set; } = MessageStatus.sent;

        public DateTime SentAt { get; set; }
        public DateTime? ReadAt { get; set; }
    }
}
