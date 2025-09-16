using ct_backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct_backend.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public int NotificationId { get; set; }

        public string UserId { get; set; } = default!;
        public virtual User User { get; set; } = default!;

        public NotificationType Type { get; set; } = NotificationType.system;
        public NotificationChannel Channel { get; set; } = NotificationChannel.inapp;
        public NotificationStatus Status { get; set; } = NotificationStatus.queued;

        [MaxLength(150)] public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;

        public DateTime? ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public string? DataJson { get; set; }
    }
}
