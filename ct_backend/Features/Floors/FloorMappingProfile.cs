using ct_backend.Domain.Entities;

namespace ct_backend.Features.Floors
{
    public class FloorMappingProfile
        : AbstractMappingProfile<Floor, FloorDto, CreateFloorRequest, UpdateFloorRequest> { }
}
