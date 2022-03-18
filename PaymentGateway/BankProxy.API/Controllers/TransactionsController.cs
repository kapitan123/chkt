using BankProxy.API.Models;
using BankProxy.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankProxy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IBank _bank;

        public TransactionsController(ILogger<TransactionsController> logger, IBank bank)
        {
            _logger = logger;
            _bank = bank;
        }

        // AK TODO should react to events as well
        // AK TODO add swagger dock
        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutResponse>> Checkout([FromBody]CheckoutRequest checkoutReq)
        {
            // AK TODO probably makes sense to have two mocked banks in the proxy
            // We get the banks by the starting num of a card.
            // even num - ShadyBank
            // odd num - PrivateBank

            // We should store the message and send an akk
            // We should retry failed transactions 
            // We should be protected from double charge
            // We should implement idempotency

            // We can get a factory to get more banks
            var bankResponse = await _bank.ProcessTransaction(checkoutReq);
            
            // AK TODO return rejected or smth
        }
    }
}