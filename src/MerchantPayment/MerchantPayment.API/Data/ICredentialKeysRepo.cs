using MerchantPayment.API.Models.Persistance;
namespace MerchantPayment.API.Data
{
    public interface ICredentialKeysRepo
    {
        Task<MerchantKey> GetById(Guid id);
    }
}
