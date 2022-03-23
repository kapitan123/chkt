using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MerchantPayment.API.Infrastructure.Middleware;

public class MerchantKeyAuthenticationHandler : AuthenticationHandler<MerchantKeyAuthenticationOptions>
{
    private const string ContentType = "application/json";
    private readonly IMerchantKeysRepository _merchantKeyRepo;
    private static readonly string HEADER_MERCHANT_KEY = "X-MERCHANT-KEY";
    public MerchantKeyAuthenticationHandler(
        IOptionsMonitor<MerchantKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IMerchantKeysRepository merchantKeyRepo) : base(options, logger, encoder, clock)
    {
        _merchantKeyRepo = merchantKeyRepo ?? throw new ArgumentNullException(nameof(merchantKeyRepo));
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(HEADER_MERCHANT_KEY, out var merchantHeaderValues))
        {
            return AuthenticateResult.Fail($"Header {HEADER_MERCHANT_KEY} is not present. Payment can't be submitted.");
        }

        var merchantApiKeyIdString = merchantHeaderValues.FirstOrDefault();

        if (merchantHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(merchantApiKeyIdString))
        {
            return AuthenticateResult.Fail($"Header {HEADER_MERCHANT_KEY} is empty. Payment can't be submitted.");
        }

        if (!Guid.TryParse(merchantApiKeyIdString, out var merchantKey))
        {
            return AuthenticateResult.Fail($"Header value for {HEADER_MERCHANT_KEY} is malformed.");
        }

        var merchantApiKey = await _merchantKeyRepo.GetByIdAsync(merchantKey);

        if(merchantApiKey == null)
        {
            return AuthenticateResult.Fail($"Merchant with this key is not found");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, merchantApiKey.Owner)
        };

        var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
        var identities = new List<ClaimsIdentity> { identity };
        var principal = new ClaimsPrincipal(identities);
        var ticket = new AuthenticationTicket(principal, Options.Scheme);

        return AuthenticateResult.Success(ticket);
    }

    protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = 403;
        Response.ContentType = ContentType;
        var problemDetails = new ErrorDetails(new string[] { "Forbidden" });

        await Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}