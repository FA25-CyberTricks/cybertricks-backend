using ct_backend.Domain.Entities;
using ct_backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct_backend.Features.Rooms
{
    public class CreateRoomRequest : AbstractRequest
    {
        public int FloorId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = default!;

        public RoomType? Type { get; set; }
        public int? Capacity { get; set; }
    }
}