using System.Collections.Generic;

namespace BankProxy.API.Services
{
    // Probably need an Interface
    public class TrustyBankMock : IBank
    {
        // AK TODO: Mocking based on the comment
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
            await Task.Delay(10);

            var response = _commentToResponseMap.GetValueOrDefault(
                           request.Message,
                           new BankResponse(0, "Success"));
            response = response with { Id = Guid.NewGuid() };

            return response;
        }

        public bool IsIssuerOf(CardNumber cardNumber)
        {
            // We should store a number
            // We should have a case for not supported issuer
            return cardNumber.BankDigits != "0000 0";
        }
    }
}
