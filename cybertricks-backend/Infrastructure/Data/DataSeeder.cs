using ct.backend.Domain.Entities;
using ct.backend.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

namespace ct.backend.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        private readonly BookingDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(
            BookingDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> SeedAllAsync()
        {
            //await _context.Database.MigrateAsync();

            //await SeedBrandsAsync();
            //await SeedStoresAsync();
            //await SeedFloorsAsync();
            //await SeedRoomsAsync();
            //await SeedMachinesAsync();
            //await SeedMenuCategoriesAsync();
            //await SeedMenuItemsAsync();
            //await SeedRolesAsync();
            //await SeedUsersAsync();
            //await SeedBrandOwnersAsync();
            //await SeedStoreManagersAsync();
            //await SeedStoreStaffsAsync();
            await SeedVouchersAsync();
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
                    Active = true,
                });
            }
            await _context.SaveChangesAsync();
        }

        // ===================== 8) USERS =====================
        private async Task SeedRolesAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Name = "Owner", NormalizedName = "OWNER" },
                    new IdentityRole { Name = "Manager", NormalizedName = "Manager" },
                    new IdentityRole { Name = "Staff", NormalizedName = "STAFF" },
                    new IdentityRole { Name = "User", NormalizedName = "USER" },
                };

                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
        }

        private async Task SeedUsersAsync()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var users = new List<(User user, string password, string role)>
                {
                    // admin
                    (
                        new User
                        {
                            UserName = "admin",
                            Email = "admin@gmail.com",
                            FullName = "System Administrator",
                            EmailConfirmed = true,
                            SubscriptionType = "Premium",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddYears(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "Admin"
                    ),

                    // brand owner
                    (
                        new User
                        {
                            UserName = "Owner1",
                            Email = "owner1@gmail.com",
                            FullName = "Owner 1",
                            EmailConfirmed = true,
                            SubscriptionType = "Basic",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddMonths(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "Owner"
                    ),
                    (
                        new User
                        {
                            UserName = "Owner2",
                            Email = "owner2@gmail.com",
                            FullName = "Owner 2",
                            EmailConfirmed = true,
                            SubscriptionType = "Basic",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddMonths(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "Owner"
                    ),

                    // store manager
                    (
                        new User
                        {
                            UserName = "manager1",
                            Email = "manager1@gmail.com",
                            FullName = "Manager 1",
                            EmailConfirmed = true,
                            SubscriptionType = "Basic",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddMonths(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "Manager"
                    ),
                    (
                        new User
                        {
                            UserName = "manager2",
                            Email = "manager2@gmail.com",
                            FullName = "Manager 2",
                            EmailConfirmed = true,
                            SubscriptionType = "Basic",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddMonths(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "Manager"
                    ),

                    // store staff
                    (
                        new User
                        {
                            UserName = "staff1",
                            Email = "staff1@gmail.com",
                            FullName = "Staff 1",
                            EmailConfirmed = true,
                            SubscriptionType = "Basic",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddMonths(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "Staff"
                    ),
                    (
                        new User
                        {
                            UserName = "staff2",
                            Email = "staff2@gmail.com",
                            FullName = "Staff 2",
                            EmailConfirmed = true,
                            SubscriptionType = "Basic",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddMonths(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "Staff"
                    ),

                    // user
                    (
                        new User
                        {
                            UserName = "User1",
                            Email = "user1@gmail.com",
                            FullName = "User 1",
                            EmailConfirmed = true,
                            SubscriptionType = "Basic",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddMonths(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "User"
                    ),
                    (
                        new User
                        {
                            UserName = "User2",
                            Email = "user2@gmail.com",
                            FullName = "User 2",
                            EmailConfirmed = true,
                            SubscriptionType = "Basic",
                            SubscriptionStartDate = DateTime.Now,
                            SubscriptionEndDate = DateTime.Now.AddMonths(1),
                            IsActive = true
                        },
                        "Abcd1234!",
                        "User"
                    )
                };

                foreach (var (user, password, role) in users)
                {
                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }

                    await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
                }
            }
        }

        private async Task SeedBrandOwnersAsync()
        {
            var cyberwave = await _context.Brands.FirstOrDefaultAsync(c => c.Code == "CYBERWAVE");
            var pixelForce = await _context.Brands.FirstOrDefaultAsync(c => c.Code == "PIXELFORGE");

            var owner1 = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Owner1");
            var owner2 = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Owner2");

            if (!await _context.BrandOwners.AnyAsync())
            {
                var brandOwners = new List<BrandOwner>
                {
                    new BrandOwner { BrandId = cyberwave.BrandId, UserId = owner1.Id },
                    new BrandOwner { BrandId = pixelForce.BrandId, UserId = owner2.Id },
                };

                await _context.BrandOwners.AddRangeAsync(brandOwners);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedStoreManagersAsync()
        {
            var cyberwaves = await _context.Stores.Where(c => c.Brand.Code == "CYBERWAVE").ToListAsync();
            var pixelForces = await _context.Stores.Where(c => c.Brand.Code == "PIXELFORGE").ToListAsync();

            var manager1 = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Manager1");
            var manager2 = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Manager2");

            if (!await _context.StoreStaffs.AnyAsync())
            {
                var storeStaffs = new List<StoreStaff>();

                foreach (var store in cyberwaves)
                {
                    storeStaffs.Add(new StoreStaff
                    {
                        StoreId = store.StoreId,
                        UserId = manager1.Id
                    });
                }

                foreach (var store in pixelForces)
                {
                    storeStaffs.Add(new StoreStaff
                    {
                        StoreId = store.StoreId,
                        UserId = manager2.Id
                    });
                }

                await _context.StoreStaffs.AddRangeAsync(storeStaffs);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedStoreStaffsAsync()
        {
            var cyberwave = await _context.Stores
               .Where(c => c.Brand.Code == "CYBERWAVE")
               .FirstOrDefaultAsync();

            var pixelForce = await _context.Stores
                .Where(c => c.Brand.Code == "PIXELFORGE")
                .FirstOrDefaultAsync();

            var staff1 = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Staff1");
            var staff2 = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == "Staff2");

            if (!await _context.StoreStaffs.AnyAsync())
            {
                var storeStaffs = new List<StoreStaff>
                {
                    new StoreStaff { StoreId = cyberwave.BrandId, UserId = staff1.Id },
                    new StoreStaff { StoreId = pixelForce.BrandId, UserId = staff2.Id },
                };

                await _context.StoreStaffs.AddRangeAsync(storeStaffs);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedVouchersAsync()
        {
            if (await _context.Vouchers.AnyAsync()) return;

            var store1 = await _context.Stores.FirstOrDefaultAsync();
            var store2 = await _context.Stores.Skip(1).FirstOrDefaultAsync();

            var vouchers = new List<Voucher>
            {
                new Voucher
                {
                    Code = "WELCOME10",
                    Description = "Giảm 10% cho khách hàng mới",
                    DiscountPercent = 10,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(3),
                    UsageLimit = 500,
                    UsedCount = 0,
                    Status = VoucherStatus.Active
                },
                new Voucher
                {
                    Code = "STORE50K",
                    Description = "Giảm 50.000đ cho đơn hàng tại Store A",
                    StoreId = store1?.StoreId,
                    DiscountAmount = 50000,
                    MinOrderAmount = 200000,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(2),
                    UsageLimit = 100,
                    Status = VoucherStatus.Active
                },
                new Voucher
                {
                    Code = "VIP20",
                    Description = "Voucher giảm 20% dành riêng cho thành viên VIP",
                    StoreId = store2?.StoreId,
                    DiscountPercent = 20,
                    MinOrderAmount = 100000,
                    MaxDiscountAmount = 100000,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1),
                    UsageLimit = 50,
                    Status = VoucherStatus.Active
                }
            };

            _context.Vouchers.AddRange(vouchers);
            await _context.SaveChangesAsync();
        }
    }
}
