namespace BankProxy.API.Services
{
    public class TrustyBankMock : IBank
    {
        private readonly Dictionary<string, BankResponse> _commentToResponseMap = new();

        public TrustyBankMock()
        {
            _commentToResponseMap.Add("blackList", new BankResponse(222, "The card is blacklisted"));
            _commentToResponseMap.Add("noMoney", new BankResponse(333, "Insufficient funds"));
            _commentToResponseMap.Add("blocked", new BankResponse(444, "The card is blocked or reissued"));
            _commentToResponseMap.Add("cardNumber", new BankResponse(555, "Invalid card number"));
        }

        public async Task<BankResponse> ProcessTransaction(CheckoutRequest request)
        {
            // Simulating bank processing
            await Task.Delay(500);

            var response = _commentToResponseMap.GetValueOrDefault(
                           request.Message,
                           new BankResponse(0, "Success"));
            response = response with { Id = Guid.NewGuid() };

            return response;
        }

        public bool IsIssuerOf(CardNumber cardNumber)
        {
            return cardNumber.BankDigits != "0000 0";
        }
    }
}
