using ct_backend.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace ct_backend.Features.Machines
{
    public class MachineDto
    {
        public int? MachineId { get; set; }

        public int? RoomId { get; set; }

        [MaxLength(50)]
        public string? Code { get; set; } = default!;
        public MachineStatus? Status { get; set; }
        public string? SpecJson { get; set; }     // JSON
    }
}