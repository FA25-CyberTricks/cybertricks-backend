using ct.backend.Domain.Entities;

namespace ct.backend.Features.Brands
{
    public class BrandMappingProfile
        : AbstractMappingProfile<Brand, BrandDto, CreateBrandRequest, UpdateBrandRequest> { }
}
