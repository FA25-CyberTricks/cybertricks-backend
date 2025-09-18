using AutoMapper;
using ct_backend.Domain.Entities;

namespace ct_backend.Features.Floors
{
    public class FloorMappingProfile : Profile
    {
        public FloorMappingProfile()
        {
            CreateMap<Floor, FloorDto>().ReverseMap();
            CreateMap<CreateFloorRequest, Floor>();
            CreateMap<UpdateFloorRequest, Floor>()
                .ForAllMembers(opt =>
                     opt.Condition((src, dest, srcMember, ctx) => srcMember != null));
        }
    }
}
