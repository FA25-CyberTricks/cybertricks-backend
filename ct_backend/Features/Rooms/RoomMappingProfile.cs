using ct_backend.Domain.Entities;

namespace ct_backend.Features.Rooms
{
    public class RoomMappingProfile
        : AbstractMappingProfile<Room, RoomDto, CreateRoomRequest, UpdateRoomRequest> { }
}
