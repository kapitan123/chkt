namespace BankProxy.API.Services
{
    public interface IBank
    {
        // AK TODO we should decouple two request and not pass it directly to bank
        public Task<BankResponse> ProcessTransaction(CheckoutRequest request);

        public bool IsIssuerOf(CardNumber cardNumber);

        // AK TODO we can retrieve credentials form a store to submit to the banks
        //public string IntegrationCredentials { get;}
    }
}
