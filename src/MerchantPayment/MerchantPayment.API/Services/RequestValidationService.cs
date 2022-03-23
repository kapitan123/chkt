namespace MerchantPayment.API.Services;

public class RequestValidationService : IRequestValidationService
{
    private readonly Regex _cardExp = new(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$");
    private readonly Regex _cvvExp = new(@"^\d{3}$");
    private readonly ISystemClock _clock;

    public RequestValidationService(ISystemClock clock)
    {
        _clock = clock;
    }

    public ValidationResult ValidateSubmitPaymentRequest(SubmitPaymentRequest req)
    {
        var errors = new List<string>();
        var details = req.CardDetails;

        if (!IsNumberValid(details.Number))
        {
            errors.Add("Card number is invalid");
        }

        if (!IsCvvSet(details.Cvv))
        {
            // should not actually expose it
            errors.Add("Card cvv is invalid");
        }

        if (!IsExpirationValid(details.Expiration))
        {
            errors.Add("Card expiration is invalid");
        }

        if (req.Sum.Amount <= 0)
        {
            errors.Add("Payment sum should be > 0");
        }

        return new ValidationResult()
        {
            IsValid = !errors.Any(),
            Errors = errors
        };
    }

    private bool IsNumberValid(string cardNumber) => _cardExp.IsMatch(cardNumber);

    private bool IsCvvSet(string cvv) => _cvvExp.IsMatch(cvv);

    private bool IsExpirationValid(DateTime expirationDate) => expirationDate > _clock.UtcNow;
}

