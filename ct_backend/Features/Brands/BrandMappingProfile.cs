using ct_backend.Domain.Entities;

namespace ct_backend.Features.Brands
{
    public class BrandMappingProfile
        : AbstractMappingProfile<Brand, BrandDto, CreateBrandRequest, UpdateBrandRequest> { }
}
