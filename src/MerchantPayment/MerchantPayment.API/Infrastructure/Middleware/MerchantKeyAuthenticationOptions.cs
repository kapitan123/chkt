using Microsoft.AspNetCore.Authentication;

namespace MerchantPayment.API.Infrastructure.Middleware;

public class MerchantKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "API Key";
    public string Scheme => DefaultScheme;
    public string AuthenticationType = DefaultScheme;
}
