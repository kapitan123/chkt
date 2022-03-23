namespace BankProxy.API.Services;

public class ShadyBankMock : IBank
{
    public bool IsIssuerOf(CardNumber cardNumber)
    {
        return cardNumber.BankDigits == "00000";
    }

    public async Task<BankResponse> ProcessTransaction(BankPayment request)
    {
        // Simulating bank processing
        await Task.Delay(500);

        var response = new BankResponse(0, "Always Sucess", true, Guid.NewGuid().ToString());

        return response;
    }
}