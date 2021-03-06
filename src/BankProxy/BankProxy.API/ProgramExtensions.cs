using Common.EventBus;

namespace BankProxy.API;

public static class ProgramExtensions
{
    public static void AddCustomHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDapr();
    }

    public static void AddBankProviders(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddSingleton<IBankFactory, BankFactory>();

        services.AddSingleton<IBank, ShadyBankMock>();
        services.AddSingleton<IBank, TrustyBankMock>();
    }

    public static void AddEventHandling(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddScoped<IEventBus, DaprEventBus>();
        services.AddScoped<IProcessedMessagesRepository, ProcessedMessagesRepositoryMock>();
        services.AddScoped<PaymentStatusChangedToReadyForExternalTransactionHandler>();
    }
}
