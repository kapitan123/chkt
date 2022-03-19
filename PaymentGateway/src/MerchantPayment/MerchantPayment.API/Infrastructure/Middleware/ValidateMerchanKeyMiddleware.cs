using MerchantPayment.API.Data;
using MerchantPayment.API.Models.Persistance;
using Microsoft.Extensions.Primitives;

namespace MerchantPayment.API.Infrastructure.Middleware
{
    public class ValidateMerchanKeyMiddleware
    {
        private static readonly string HEADER_MERCHANT_KEY = "X-MERCHANT-KEY";
        private readonly ICredentialKeysRepo _merchants;

        public ValidateMerchanKeyMiddleware(ICredentialKeysRepo merchants)
        {
            _merchants = merchants;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;

            var merchantKey = ExtractMerchantId(headers);

            // AK TODO we should cashe this call
            var merchant = await _merchants.GetById(merchantKey);

            ValidateExpirationAndState(merchant);
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
                throw new ArgumentException($"Header value for {HEADER_MERCHANT_KEY} is inactive or expired.");
            }
        }
    }
}
