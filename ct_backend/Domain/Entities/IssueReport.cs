using ct_backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct_backend.Domain.Entities
{
    public class IssueReport : BaseEntity
    {
        public int IssueId { get; set; }

        public string ReporterId { get; set; } = default!;
        public virtual User Reporter { get; set; } = default!;

        public IssueScope Scope { get; set; } = IssueScope.store;
        public IssueCategory Category { get; set; } = IssueCategory.other;
        public IssuePriority Priority { get; set; } = IssuePriority.medium;

        [MaxLength(200)] 
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public IssueStatus Status { get; set; } = IssueStatus.open;

        [MaxLength(20)] 
        public string Source { get; set; } = "web";
    }
}
