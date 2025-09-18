using ct_backend.Domain.Entities;
using ct_backend.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                .AddEntityFrameworkStores<BookingDbContext>()
                .AddDefaultTokenProviders();

            _ = services
                 .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = config["Jwt:Issuer"],
                         ValidAudience = config["Jwt:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]
                            ?? throw new InvalidOperationException("Missing:Jwt:Key")))
                     };
                // })
                //.AddGoogle(options =>
                //{
                //    options.ClientId = config["Authentication:Google:ClientId"]
                //        ?? throw new InvalidOperationException("Missing Google:ClientId"); ;
                //    options.ClientSecret = config["Authentication:Google:ClientSecret"]
                //        ?? throw new InvalidOperationException("Missing Google:ClientSecret"); ;
                //    options.CallbackPath = "/signin-google";
                //    // Explicitly request the profile scope
                //    options.Scope.Add("profile");

                //    // Map the picture to a claim
                //    options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                });
            return services;
        }
    }
}