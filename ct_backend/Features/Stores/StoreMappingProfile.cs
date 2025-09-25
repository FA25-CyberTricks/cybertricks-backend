using ct_backend.Domain.Entities;

namespace ct_backend.Features.Stores
{
    public class StoreMappingProfile
        : AbstractMappingProfile<Store, StoreDto, CreateStoreRequest, UpdateStoreRequest> { }
}
