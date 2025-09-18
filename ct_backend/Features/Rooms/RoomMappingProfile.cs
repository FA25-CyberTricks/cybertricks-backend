using AutoMapper;
using ct_backend.Domain.Entities;

namespace ct_backend.Features.Rooms
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<UpdateRoomRequest, Room>()
                .ForAllMembers(opt =>
                     opt.Condition((src, dest, srcMember, ctx) => srcMember != null));
        }
    }
}
