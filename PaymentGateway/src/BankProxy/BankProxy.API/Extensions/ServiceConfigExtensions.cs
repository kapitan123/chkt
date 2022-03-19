namespace BankProxy.API.Extensions
{
    public static class ServiceConfigExtensions
    {
        public static IServiceCollection AddBankProviders(this IServiceCollection services)
        {
            services.AddSingleton<IBankFactory, BankFactory>();

            services.AddSingleton<IBank, ShadyBankMock>();
            services.AddSingleton<IBank, TrustyBankMock>();

            return services;
        }
    }
}
