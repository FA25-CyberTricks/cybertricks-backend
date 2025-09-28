using ct.backend.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ct.backend.Infrastructure.Data
{
    public class BookingDbContext : IdentityDbContext<User>
    {
        // Core entities
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreImage> StoreImages { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Machine> Machines { get; set; }

        // Booking related entities
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingMachine> BookingMachines { get; set; }

        // User related entities
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        // Order related entities
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // Payment and invoice related entities
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        // Other entities
        public DbSet<Review> Reviews { get; set; }
        public DbSet<IssueReport> IssueReports { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        // User
        public DbSet<BrandOwner> BrandOwners { get; set; }
        public DbSet<StoreStaff> StoreStaffs { get; set; }

        // Additional
        public DbSet<StoreAccount> StoreAccounts { get; set; }
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


        public BookingDbContext(DbContextOptions<BookingDbContext> options)
         : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);

            RemoveAspNetPrefixInIdentityTable(builder: builder);
        }

        private static void RemoveAspNetPrefixInIdentityTable(ModelBuilder builder)
        {
            const string AspNetPrefix = "AspNet";

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();

                if (tableName.StartsWith(value: AspNetPrefix))
                {
                    entityType.SetTableName(name: tableName[6..]);
                }
            }
        }
    }
}
