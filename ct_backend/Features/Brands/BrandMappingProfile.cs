using AutoMapper;
using ct_backend.Domain.Entities;

namespace ct_backend.Features.Brands
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<CreateBrandRequest, Brand>();
            CreateMap<UpdateBrandRequest, Brand>()
                .ForAllMembers(opt => 
                     opt.Condition((src, dest, srcMember, ctx) => srcMember != null));
        }
    }
}