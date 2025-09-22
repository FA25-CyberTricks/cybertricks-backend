using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ct_backend.Domain.Entities;
using ct_backend.Domain.Enum;

namespace ct_backend.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        private readonly BookingDbContext _context;

        public DatabaseSeeder(BookingDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SeedAllAsync()
        {
            await _context.Database.MigrateAsync();

            await SeedBrandsAsync();
            await SeedStoresAsync();
            await SeedFloorsAsync();
            await SeedRoomsAsync();
            await SeedMachinesAsync();
            await SeedMenuCategoriesAsync();
            await SeedMenuItemsAsync();

            return true;
        }

        // ===================== 1) BRANDS =====================
        private static readonly (string Code, string Name, string Email, string Phone, string Desc, bool IsLarge)[] BrandData =
        {
            ("CYBERWAVE",  "CyberWave Esports",  "contact@cyberwave.vn", "0901234567",
                "Chuỗi phòng game cao cấp, định hướng eSports.", true),
            ("PIXELFORGE", "PixelForge Gaming",  "hello@pixelforge.vn",  "0907654321",
                "Chuỗi phòng game quy mô vừa và nhỏ, giá dễ tiếp cận.", false)
        };

        private async Task SeedBrandsAsync()
        {
            foreach (var b in BrandData)
            {
                var exists = await _context.Brands.AnyAsync(x => x.Code == b.Code);
                if (exists) continue;

                await _context.Brands.AddAsync(new Brand
                {
                    Code = b.Code,
                    Name = b.Name,
                    ContactEmail = b.Email,
                    ContactPhone = b.Phone,
                    Description = b.Desc,
                    Status = BrandStatus.active,
                    AvgRating = b.IsLarge ? 4.6 : 4.2,
                    RatingCount = b.IsLarge ? 128 : 74
                });
            }
            await _context.SaveChangesAsync();
        }

        // ===================== 2) STORES =====================
        private sealed record StoreSeed(string BrandCode, string Name, string Address, bool IsLarge);
        private static readonly StoreSeed[] StoreData =
        {
            new("CYBERWAVE", "CyberWave Q1 Flagship", "12 Nguyễn Huệ, Q1, TP.HCM", true),
            new("CYBERWAVE", "CyberWave Q7",          "99 Nguyễn Văn Linh, Q7, TP.HCM", false),

            new("PIXELFORGE","PixelForge Đà Nẵng",     "50 Bạch Đằng, Hải Châu, Đà Nẵng", false),
            new("PIXELFORGE","PixelForge Hà Nội",      "210 Xã Đàn, Đống Đa, Hà Nội", true),
        };

        private async Task SeedStoresAsync()
        {
            var brands = await _context.Brands.ToListAsync();
            foreach (var s in StoreData)
            {
                var brand = brands.First(b => b.Code == s.BrandCode);
                var exists = await _context.Stores.AnyAsync(x => x.Name == s.Name && x.BrandId == brand.BrandId);
                if (exists) continue;

                await _context.Stores.AddAsync(new Store
                {
                    BrandId = brand.BrandId,
                    Name = s.Name,
                    Address = s.Address,
                    ContactPhone = "1900-1234",
                    Status = StoreStatus.active,
                    DisplayOrder = s.IsLarge ? 1 : 2
                });
            }
            await _context.SaveChangesAsync();
        }

        // ===================== 3) FLOORS =====================
        private sealed record FloorSeed(string StoreName, int FloorNumber, string? Name);
        private static readonly FloorSeed[] FloorData =
        {
            new("CyberWave Q1 Flagship", 1, "Tầng 1 (Tiếp tân)"),
            new("CyberWave Q1 Flagship", 2, "Tầng 2 (Luyện tập)"),
            new("CyberWave Q1 Flagship", 3, "Tầng 3 (Thi đấu)"),

            new("CyberWave Q7", 1, "Tầng trệt"),
            new("CyberWave Q7", 2, "Lầu 1"),

            new("PixelForge Đà Nẵng", 1, "Tầng 1"),
            new("PixelForge Đà Nẵng", 2, "Tầng 2"),

            new("PixelForge Hà Nội", 1, "Tầng 1"),
            new("PixelForge Hà Nội", 2, "Tầng 2"),
            new("PixelForge Hà Nội", 3, "Tầng 3"),
        };

        private async Task SeedFloorsAsync()
        {
            var stores = await _context.Stores.ToListAsync();
            foreach (var f in FloorData)
            {
                var store = stores.First(s => s.Name == f.StoreName);
                var exists = await _context.Floors.AnyAsync(x => x.StoreId == store.StoreId && x.FloorNumber == f.FloorNumber);
                if (exists) continue;

                await _context.Floors.AddAsync(new Floor
                {
                    StoreId = store.StoreId,
                    FloorNumber = f.FloorNumber,
                    Name = f.Name,
                    Status = FloorStatus.active,
                    DisplayOrder = f.FloorNumber
                });
            }
            await _context.SaveChangesAsync();
        }

        // ===================== 4) ROOMS =====================
        private sealed record RoomSeed(string StoreName, int FloorNumber, string RoomName, RoomType Type, int MachineCount, bool IsVip);
        private static readonly RoomSeed[] RoomData =
        {
            new("CyberWave Q1 Flagship", 1, "CW1-STD-01", RoomType.normal, 8, false),
            new("CyberWave Q1 Flagship", 1, "CW1-STD-02", RoomType.normal, 8, false),
            new("CyberWave Q1 Flagship", 2, "CW1-VIP-01", RoomType.vip,      6, true),
            new("CyberWave Q1 Flagship", 2, "CW1-STD-03", RoomType.normal, 10, false),
            new("CyberWave Q1 Flagship", 3, "CW1-ARENA",  RoomType.vip,      10, true),

            new("CyberWave Q7", 1, "CW7-STD-01", RoomType.normal, 7, false),
            new("CyberWave Q7", 1, "CW7-VIP-01", RoomType.vip,      5, true),
            new("CyberWave Q7", 2, "CW7-STD-02", RoomType.normal, 6, false),

            new("PixelForge Đà Nẵng", 1, "PFDN-STD-01", RoomType.normal, 8, false),
            new("PixelForge Đà Nẵng", 2, "PFDN-VIP-01", RoomType.vip,      5, true),
            new("PixelForge Đà Nẵng", 2, "PFDN-STD-02", RoomType.normal, 6, false),

            new("PixelForge Hà Nội", 1, "PFHN-STD-01", RoomType.normal, 9, false),
            new("PixelForge Hà Nội", 2, "PFHN-STD-02", RoomType.normal, 9, false),
            new("PixelForge Hà Nội", 2, "PFHN-VIP-01", RoomType.vip,      6, true),
            new("PixelForge Hà Nội", 3, "PFHN-VIP-ARENA", RoomType.vip,   8, true),
        };

        private async Task SeedRoomsAsync()
        {
            // Map (StoreName,FloorNumber) -> FloorId
            var floors = await _context.Floors.Include(f => f.Store).ToListAsync();

            foreach (var r in RoomData)
            {
                var floor = floors.First(f => f.Store!.Name == r.StoreName && f.FloorNumber == r.FloorNumber);
                var exists = await _context.Rooms.AnyAsync(x => x.FloorId == floor.FloorId && x.Name == r.RoomName);
                if (exists) continue;

                await _context.Rooms.AddAsync(new Room
                {
                    FloorId = floor.FloorId,
                    Name = r.RoomName,
                    Type = r.Type,
                    Capacity = r.IsVip ? 10 : 8,
                    Status = RoomStatus.active,
                    DisplayOrder = r.IsVip ? 1 : 2
                });
            }
            await _context.SaveChangesAsync();
        }

        // ===================== 5) MACHINES (specJson) =====================
        private async Task SeedMachinesAsync()
        {
            var rooms = await _context.Rooms.ToListAsync();

            foreach (var r in RoomData)
            {
                var room = rooms.First(x => x.Name == r.RoomName);
                // nếu đã đủ số lượng tối thiểu thì bỏ qua
                var current = await _context.Machines.CountAsync(m => m.RoomId == room.RoomId);
                if (current >= r.MachineCount) continue;

                var need = r.MachineCount - current;
                for (int i = current + 1; i <= current + need; i++)
                {
                    var code = $"{r.RoomName}-PC-{i:D2}";
                    var spec = r.IsVip ? BuildSpecJsonHighEnd(i) : BuildSpecJsonStandard(i);

                    // tránh trùng code nếu chạy nhiều lần
                    var exists = await _context.Machines.AnyAsync(m => m.Code == code);
                    if (exists) continue;

                    await _context.Machines.AddAsync(new Machine
                    {
                        RoomId = room.RoomId,
                        Code = code,
                        Status = MachineStatus.available,
                        SpecJson = spec
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        private static string BuildSpecJsonHighEnd(int index)
        {
            var spec = new
            {
                cpu = "Intel Core i7-12700F",
                gpu = "NVIDIA GeForce RTX 4070 12GB",
                ram = "32GB DDR4-3600",
                storage = new[] { "1TB NVMe SSD" },
                monitor = "27\" 2K 165Hz IPS",
                peripherals = new
                {
                    keyboard = "Mechanical TKL (RGB)",
                    mouse = "Logitech G Pro / Razer Viper",
                    headset = "HyperX Cloud II",
                    chair = "Ghế công thái học cao cấp"
                },
                os = "Windows 11 Pro",
                extras = new[] { "Bàn rộng", "Đèn ambient RGB", "Cách âm tốt" },
                note = $"VIP Rig #{index}"
            };
            return JsonSerializer.Serialize(spec);
        }

        private static string BuildSpecJsonStandard(int index)
        {
            var spec = new
            {
                cpu = "Intel Core i5-12400F",
                gpu = "NVIDIA GeForce RTX 3060 12GB",
                ram = "16GB DDR4-3200",
                storage = new[] { "512GB NVMe SSD" },
                monitor = "24\" 1080p 144Hz",
                peripherals = new
                {
                    keyboard = "Mem-Mechanical (RGB)",
                    mouse = "Logitech G102",
                    headset = "Onikuma K9",
                    chair = "Ghế lưng cao"
                },
                os = "Windows 11 Home",
                extras = new[] { "Bàn vừa", "Đèn nền cơ bản" },
                note = $"Standard Rig #{index}"
            };
            return JsonSerializer.Serialize(spec);
        }

        // ===================== 6) MENU CATEGORIES =====================
        private sealed record CatSeed(string BrandCode, string Name);
        private static readonly CatSeed[] CatData =
        {
            new("CYBERWAVE", "Combo"),
            new("CYBERWAVE", "Đồ uống"),
            new("CYBERWAVE", "Snack"),

            new("PIXELFORGE", "Combo"),
            new("PIXELFORGE", "Đồ uống"),
            new("PIXELFORGE", "Snack"),
        };

        private async Task SeedMenuCategoriesAsync()
        {
            var brands = await _context.Brands.ToListAsync();

            foreach (var c in CatData)
            {
                var brand = brands.First(b => b.Code == c.BrandCode);
                var exists = await _context.MenuCategories.AnyAsync(x => x.BrandId == brand.BrandId && x.Name == c.Name);
                if (exists) continue;

                await _context.MenuCategories.AddAsync(new MenuCategory
                {
                    BrandId = brand.BrandId,
                    Name = c.Name,
                    Active = true
                });
            }
            await _context.SaveChangesAsync();
        }

        // ===================== 7) MENU ITEMS =====================
        private sealed record ItemSeed(string BrandCode, string CategoryName, string ItemName, decimal Price);
        private static readonly ItemSeed[] ItemData =
        {
            // CYBERWAVE
            new("CYBERWAVE","Combo",    "Combo Năng Lượng (RedBull + Snack)", 49000),
            new("CYBERWAVE","Combo",    "Combo Try–Hard (Cà phê sữa + Mì ly)", 59000),
            new("CYBERWAVE","Đồ uống",  "Cà phê sữa đá", 25000),
            new("CYBERWAVE","Đồ uống",  "Trà đào cam sả", 35000),
            new("CYBERWAVE","Đồ uống",  "Nước suối", 10000),
            new("CYBERWAVE","Snack",    "Mì ly cay", 19000),
            new("CYBERWAVE","Snack",    "Khoai tây chiên", 29000),

            // PIXELFORGE
            new("PIXELFORGE","Combo",   "Combo Sinh viên (Trà chanh + Snack)", 39000),
            new("PIXELFORGE","Đồ uống", "Trà chanh đào", 22000),
            new("PIXELFORGE","Đồ uống", "Soda bạc hà", 28000),
            new("PIXELFORGE","Snack",   "Xúc xích nướng", 25000),
            new("PIXELFORGE","Snack",   "Bánh mì pate", 20000),
        };

        private async Task SeedMenuItemsAsync()
        {
            var brands = await _context.Brands.ToListAsync();
            var cats = await _context.MenuCategories.ToListAsync();

            foreach (var it in ItemData)
            {
                var brand = brands.First(b => b.Code == it.BrandCode);
                var cat = cats.First(c => c.BrandId == brand.BrandId && c.Name == it.CategoryName);

                var exists = await _context.MenuItems.AnyAsync(x => x.BrandId == brand.BrandId && x.CategoryId == cat.CategoryId && x.Name == it.ItemName);
                if (exists) continue;

                await _context.MenuItems.AddAsync(new MenuItem
                {
                    BrandId = brand.BrandId,
                    CategoryId = cat.CategoryId,
                    Name = it.ItemName,
                    Price = it.Price,
                    Active = true
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
