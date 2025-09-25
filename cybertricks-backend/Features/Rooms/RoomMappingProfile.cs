using ct.backend.Domain.Entities;

namespace ct.backend.Features.Rooms
{
    public class RoomMappingProfile
        : AbstractMappingProfile<Room, RoomDto, CreateRoomRequest, UpdateRoomRequest> { }
}
