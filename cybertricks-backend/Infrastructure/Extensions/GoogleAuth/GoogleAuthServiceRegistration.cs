using ct.backend.Features.Accounts.Ports.GoogleAuth;
using ct.backend.Infrastructure.ExternalServices.Auth;
using Microsoft.AspNetCore.Authentication;

namespace ct.backend.Infrastructure.Extensions.GoogleAuth;
public static class GoogleAuthServiceRegistration
{
    public static IServiceCollection AddGoogleAuthService(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        //var googleAuthSettings = new GoogleAuthSettings();
        //config.GetSection("Authentication:Google").Bind(googleAuthSettings);

        //services.AddAuthentication(
        //    options =>
        //    {
        //        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        //        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        //    })       
        //    .AddGoogle(options =>
        //    {
        //        options.ClientId = googleAuthSettings.ClientId;
        //        options.ClientSecret = googleAuthSettings.ClientSecret;
        //        options.CallbackPath = "/signin-google";
        //    });

        services
        .AddAuthentication()
        .AddGoogle(options =>
        {
            options.ClientId = config["Authentication:Google:ClientId"]
                ?? throw new InvalidOperationException("Missing Google:ClientId"); ;
            options.ClientSecret = config["Authentication:Google:ClientSecret"]
                ?? throw new InvalidOperationException("Missing Google:ClientSecret"); ;
            options.CallbackPath = "/signin-google";
            // Explicitly request the profile scope
            options.Scope.Add("profile");

            // Map the picture to a claim
            options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
        });

        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        return services;
    }
}
