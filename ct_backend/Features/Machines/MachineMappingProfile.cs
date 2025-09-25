using ct_backend.Domain.Entities;

namespace ct_backend.Features.Machines
{
    public class MachineMappingProfile
        : AbstractMappingProfile<Machine, MachineDto, CreateMachineRequest, UpdateMachineRequest> { }
}