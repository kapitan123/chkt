namespace BankProxy.API.Services;

public class TrustyBankMock : IBank
{
    private readonly Dictionary<string, BankResponse> _commentToResponseMap = new();

    public TrustyBankMock()
    {
        _commentToResponseMap.Add("blackList", new BankResponse(222, "The card is blacklisted", false));
        _commentToResponseMap.Add("noMoney", new BankResponse(333, "Insufficient funds", false));
        _commentToResponseMap.Add("blocked", new BankResponse(444, "The card is blocked or reissued", false));
    }

    public async Task<BankResponse> ProcessTransaction(BankPayment request)
    {
        // Simulating bank processing
        await Task.Delay(500);

        var response = _commentToResponseMap.GetValueOrDefault(
                            request.Message,
                            new BankResponse(0, "Success", true));

        response = response with { BankReference = Guid.NewGuid().ToString()};

        return response;
    }

    public bool IsIssuerOf(CardNumber cardNumber)
    {
        return cardNumber.BankDigits != "00000";
    }
}
