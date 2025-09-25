using ct.backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct.backend.Domain.Entities
{
    public class Floor : BaseEntity
    {
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; } = default!;

        [MaxLength(200)] 
        public string? Name { get; set; }

        public FloorStatus Status { get; set; } = FloorStatus.active;
        public int DisplayOrder { get; set; } = 0;

        public virtual IEnumerable<Room> Rooms { get; set; }
    }
}
