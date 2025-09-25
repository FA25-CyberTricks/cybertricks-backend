using AutoMapper;

namespace ct_backend.Features
{
    public abstract class AbstractMappingProfile<TEntity, TDto, TCreateRequest, TUpdateRequest> : Profile
    {
        protected AbstractMappingProfile()
        {
            CreateMap<TEntity, TDto>().ReverseMap();
            CreateMap<TCreateRequest, TEntity>();
            CreateMap<TUpdateRequest, TEntity>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, ctx) =>
                {
                    if (srcMember == null) return false;
                    if (srcMember is string s) return !string.IsNullOrWhiteSpace(s);
                    return true;
                }));
        }
    }
}
