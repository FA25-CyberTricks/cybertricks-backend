namespace ct.backend.Features.Stores
{
    public class QueryStoreRequest : AbstractRequest
    {
        // --- Pagination ---
        public int PageIndex { get; set; } = 1; // mặc định trang đầu
        public int PageSize { get; set; } = 10; // mặc định 10 item/trang

        // --- Sorting ---
        public string SortBy { get; set; } = "DisplayOrder"; // field sắp xếp mặc định
        public bool Desc { get; set; } = false; // true = giảm dần

        // --- Search ---
        public string q { get; set; } = string.Empty; // từ khóa tìm kiếm (theo Name, Code, Address...)

        // --- Filters ---
        public int? BrandId { get; set; } // lọc theo brand (chuỗi hệ thống cyber game)
        public string? Status { get; set; } // Active, Inactive...
        public double? MinRating { get; set; } // lọc theo điểm đánh giá tối thiểu
        public double? MaxPrice { get; set; } // lọc theo giá dịch vụ (nếu có)

        // --- Location-based filter ---
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? RadiusKm { get; set; } // bán kính tìm kiếm (km)

        // --- Other ---
        public bool OnlyOpenNow { get; set; } = false; // chỉ hiển thị quán đang mở cửa
        public bool IncludeRelations { get; set; } = false; // có lấy kèm rooms, machines... không
    }
}
