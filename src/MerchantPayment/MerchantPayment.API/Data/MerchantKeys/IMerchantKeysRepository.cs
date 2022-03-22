namespace MerchantPayment.API.Data;

public interface IMerchantKeysRepository
{
    Task<MerchantKey> GetByIdAsync(Guid id);
}
