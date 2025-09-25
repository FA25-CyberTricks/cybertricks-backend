using ct_backend.Domain.Entities;
using ct_backend.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ct_backend.Infrastructure.Extension.Database
{
    public static class DatabseServiceRegistration
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            var connectionString =
                config.GetConnectionString("sqlConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found."
                );

            services.AddDbContext<BookingDbContext>(options => options.UseMySQL(connectionString));
            // Cấu hình Identity
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequiredLength = 6;
                    options.User.RequireUniqueEmail = true;

                    // Lockout configuration
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Locked for 5 minutes
                    options.Lockout.MaxFailedAccessAttempts = 5; // Lock after 5 failed attempts
                    options.Lockout.AllowedForNewUsers = true;

                    // User configuration
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                    // SignIn configuration
                    options.SignIn.RequireConfirmedEmail = true; // Email must exist
                    options.SignIn.RequireConfirmedPhoneNumber = false; //
                })
                //.AddRoles<IdentityRole>() 
                .AddEntityFrameworkStores<BookingDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}