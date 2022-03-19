using System.Collections.Generic;

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
            await Task.Delay(10);

            var response = new BankResponse(0, "Always Sucess");

            return response;
        }
    }
}
