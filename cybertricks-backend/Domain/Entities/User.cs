using ct.backend.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [MaxLength(255)]
        public string? AvatarUrl { get; set; }

        [MaxLength(10)]
        public string? SubscriptionType { get; set; }

        public DateTime? SubscriptionStartDate { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public decimal Balance { get; set; } = 0m;

        public bool? IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLogin { get; set; }

        // Navigation properties for relationships
        public virtual IEnumerable<BrandOwner> BrandOwners { get; set; }
        public virtual IEnumerable<StoreStaff> StoreStaffs { get; set; }
        public virtual IEnumerable<Booking> Bookings { get; set; }

    }
}