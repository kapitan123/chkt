﻿using MerchantPayment.API.Infrastructure.Middleware;

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
        services.AddSingleton<ISystemClock, SystemClock>();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IEventBus, DaprEventBus>();
    }

    public static void AddMerchantKeyAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication
            (o => o.DefaultAuthenticateScheme = MerchantKeyAuthenticationOptions.DefaultScheme)
            .AddScheme<MerchantKeyAuthenticationOptions, MerchantKeyAuthenticationHandler>
                (MerchantKeyAuthenticationOptions.DefaultScheme, options => { });
    }
}
