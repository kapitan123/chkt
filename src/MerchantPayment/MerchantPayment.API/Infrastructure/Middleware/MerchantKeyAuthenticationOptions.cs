using Microsoft.AspNetCore.Authentication;

namespace MerchantPayment.API.Infrastructure.Middleware;

public class MerchantKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "Merchant API Key";
    public string Scheme => DefaultScheme;
    public string AuthenticationType = DefaultScheme;
}
