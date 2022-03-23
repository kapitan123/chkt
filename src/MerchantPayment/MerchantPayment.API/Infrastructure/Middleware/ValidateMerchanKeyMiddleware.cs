using Microsoft.Extensions.Primitives;
using System.Net;

namespace MerchantPayment.API.Infrastructure.Middleware;

public class ValidateMerchanKeyMiddleware
{
    private static readonly string HEADER_MERCHANT_KEY = "X-MERCHANT-KEY";
    private readonly IMerchantKeysRepository _merchants;
    private readonly ILogger<ValidateMerchanKeyMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ValidateMerchanKeyMiddleware(RequestDelegate next, IMerchantKeysRepository merchants, 
        ILogger<ValidateMerchanKeyMiddleware> logger)
    {
        _merchants = merchants;
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            var headers = httpContext.Request.Headers;

            var merchantKey = ExtractMerchantId(headers);

            var merchant = await _merchants.GetByIdAsync(merchantKey);

            ValidateExpirationAndState(merchant);

            await _next(httpContext);
        }
        catch (ArgumentException ex)
        {
            _logger.LogInformation(message: ex.Message);

            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await httpContext.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync("The authentication procedure has failed.");
        }
    }

    private static Guid ExtractMerchantId(IHeaderDictionary headers)
    {
        headers.TryGetValue(HEADER_MERCHANT_KEY, out var merchantKeyHeader);

        if (merchantKeyHeader == StringValues.Empty)
        {
            throw new ArgumentException($"Header value for {HEADER_MERCHANT_KEY} is not set. Payment can't be submitted.");
        }

        var isParsable = Guid.TryParse(merchantKeyHeader, out var merchantKey);

        if (!isParsable)
        {
            throw new ArgumentException($"Header value for {HEADER_MERCHANT_KEY} is malformed.");
        }

        return merchantKey;
    }

    private static void ValidateExpirationAndState(MerchantKey merchant)
    {
        if (!merchant.IsActive || merchant.ExpiratioDate < DateTime.Now)
        {
            throw new ArgumentException($"{HEADER_MERCHANT_KEY} is inactive or expired.");
        }
    }
}

