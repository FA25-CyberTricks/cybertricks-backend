using AutoMapper;
using ct_backend.Domain.Entities;

namespace ct_backend.Features.Machines
{
    public class MachineMappingProfile : Profile
    {
        public MachineMappingProfile()
        {
            CreateMap<Machine, MachineDto>().ReverseMap();
            CreateMap<CreateMachineRequest, Machine>();
            CreateMap<UpdateMachineRequest, Machine>()
                .ForAllMembers(opt =>
                     opt.Condition((src, dest, srcMember, ctx) => srcMember != null));
        }
    }
}
