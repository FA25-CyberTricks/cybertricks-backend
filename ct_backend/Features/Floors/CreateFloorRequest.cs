using System.ComponentModel.DataAnnotations;

namespace ct_backend.Features.Floors
{
    public class CreateFloorRequest : AbstractRequest
    {
        public int FloorNumber { get; set; }

        public int StoreId { get; set; }

        [MaxLength(200)]
        public string? Name { get; set; }
    }
}