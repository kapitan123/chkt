namespace MerchantPayment.API.Services;

public class ValidationService : IValidationService
{
    private readonly Regex _cardExp = new(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$");
    private readonly Regex _cvvExp = new(@"^\d{3}$");
    private readonly ISystemClock _clock;

    public ValidationService(ISystemClock clock)
    {
        _clock = clock;
    }

    public ValidationResult Validate(CardDetails details)
    {
        var errors = new List<string>();
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

