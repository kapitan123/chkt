namespace BankProxy.API.Services
{
    public interface IBank
    {
        public Task<BankResponse> ProcessTransaction(BankPayment bankPayment);

        public bool IsIssuerOf(CardNumber cardNumber);
    }
}
