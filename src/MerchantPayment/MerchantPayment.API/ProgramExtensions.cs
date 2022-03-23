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
    }

    public static IApplicationBuilder UseValidateMerchantKey(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidateMerchanKeyMiddleware>();
    }  
}
