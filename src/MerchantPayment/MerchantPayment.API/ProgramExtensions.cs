using MerchantPayment.API.Infrastructure.Middleware;

namespace MerchantPayment.API;

public static class ProgramExtensions
{
    public static void AddCustomHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDapr();
    }

    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddSingleton<IPaymentsRepository, PaymentsRepository>();
        services.AddSingleton<IMerchantKeysRepository, MerchantKeysRepositoryMock>();
        services.AddTransient<IValidationService, ValidationService>();
    }

    public static IApplicationBuilder UseValidateMerchantKey(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidateMerchanKeyMiddleware>();
    }

    public static void AddMerchantKeyAuthentication(this WebApplicationBuilder builder)
    {
        // AK TODO DefaultChallengeScheme is redundant
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = MerchantKeyAuthenticationOptions.DefaultScheme;
            options.DefaultChallengeScheme = MerchantKeyAuthenticationOptions.DefaultScheme;
        })
        .AddScheme<MerchantKeyAuthenticationOptions, MerchantKeyAuthenticationHandler>(MerchantKeyAuthenticationOptions.DefaultScheme, options => { });
    }
}
