using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankProxy.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IBankFactory _bankProvidersFactory;

        public TransactionsController(ILogger<TransactionsController> logger, IBankFactory bankProvidersFactory)
        {
            _logger = logger;
            _bankProvidersFactory = bankProvidersFactory;
        }

        // AK TODO should react to events as well
        // AK TODO Handle all exceptions and wrap them in ErrorDetails
        [HttpPost("checkout")]
        [ProducesResponseType(typeof(CheckoutResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CheckoutResponse>> Checkout([FromBody]CheckoutRequest checkoutReq)
        {
            // We should store the message and send an akk
            // We should retry failed transactions 
            // We should be protected from double charge
            // We should implement idempotency
            // We should have it in the state storage
            try
            {
                var bank = _bankProvidersFactory.GetBankByCardNumber(checkoutReq.CardDetails.CardNumber);

                var bankProcessingResult = await bank.ProcessTransaction(checkoutReq);

                var response = new CheckoutResponse
                {
                    Message = bankProcessingResult.Message,
                    IsSuccess = bankProcessingResult.StatusCode == 0,
                    Code = bankProcessingResult.StatusCode
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDetails(ex.Message));
            }
        }
    }
}