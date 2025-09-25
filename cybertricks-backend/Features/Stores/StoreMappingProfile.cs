using ct.backend.Domain.Entities;

namespace ct.backend.Features.Stores
{
    public class StoreMappingProfile
        : AbstractMappingProfile<Store, StoreDto, CreateStoreRequest, UpdateStoreRequest> { }
}
