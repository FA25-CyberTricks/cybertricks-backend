using ct_backend.Infrastructure.Extension.Database;
using ct_backend.Infrastructure.Extension.Jwt;
using ct_backend.Infrastructure.Extension.Mail;
using ct_backend.Infrastructure.Extension.Swagger;
using ct_backend.Infrastructure.Extensions.GoogleAuth;

namespace ct_backend.Infrastructure.Extension
{
    public static class CoreServiceRegistration
    {
        public static IServiceCollection AddCoreInfrastructure(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddDatabase(config);
            services.AddJwtAuthentication(config);
            services.AddMailService(config);
            services.AddGoogleAuthService(config);
            //services.AddStorageService(config);
            //services.AddRabbitMq(config);
            //services.AddPayment(config);
            //services.AddHuggingfaceService();
            services.AddSwaggerWithAuth();
            services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }

    }
}
