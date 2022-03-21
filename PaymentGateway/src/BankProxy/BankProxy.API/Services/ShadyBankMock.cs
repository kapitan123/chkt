namespace BankProxy.API.Services
{
    public class ShadyBankMock : IBank
    {
        public bool IsIssuerOf(CardNumber cardNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<BankResponse> ProcessTransaction(CheckoutRequest request)
        {
            // Simulating bank processing
            await Task.Delay(500);

            var response = new BankResponse(0, "Always Sucess");

            return response;
        }
    }
}
