using AutoMapper;
using ct_backend.Domain.Entities;

namespace ct_backend.Features.Stores
{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<CreateStoreRequest, Store>();
            CreateMap<UpdateStoreRequest, Store>()
                .ForAllMembers(opt =>
                     opt.Condition((src, dest, srcMember, ctx) => srcMember != null));
        }
    }
}
