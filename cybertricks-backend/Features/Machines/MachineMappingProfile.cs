using ct.backend.Domain.Entities;

namespace ct.backend.Features.Machines
{
    public class MachineMappingProfile
        : AbstractMappingProfile<Machine, MachineDto, CreateMachineRequest, UpdateMachineRequest> { }
}