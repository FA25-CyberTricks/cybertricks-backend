using ct_backend.Infrastructure.Extension.Database;

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
            //services.AddMailService(config);
            //services.AddGoogleAuthService(config);
            //services.AddStorageService(config);
            //services.AddRabbitMq(config);
            //services.AddPayment(config);
            //services.AddHuggingfaceService();


            //services.AddScoped<IUnitOfWork, MySQLUnitOfWork>();

            //services.AddScopedServicesByConvention(typeof(IMovieService).Assembly);

            //services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }
    }
}
