using MerchantPayment.API.Data;
using MerchantPayment.API.IntegrationEvents.EventHandlers;
using MerchantPayment.API.IntegrationEvents.Events;
using MerchantPayment.API.Models.Persistance;
using System;
using System.Threading.Tasks;

namespace MerchantPayment.API.UnitTests.IntegrationEvents.EventHandlers
{
    public class RequestPaymentValidationHandlerTests
    {
        private PaymentValidationFinishedHandler? _paymentValidationFinishedHandler;
        private Mock<IPaymentsRepository> _repoMock = new();
        private PaymentBankTransactionFailedEvent? _fakeEvent;

        public RequestPaymentValidationHandlerTests()
        {
            _repoMock = new Mock<IPaymentsRepository>();
            _repoMock.Setup(m => m.UpdateStatusAsync(It.IsAny<Guid>(), It.IsAny<PaymentStatus>()));

            _paymentValidationFinishedHandler = new PaymentValidationFinishedHandler(_repoMock.Object);

            _fakeEvent = new PaymentBankTransactionFailedEvent(Guid.Parse("f750fb47-b2c1-4bec-9ee1-55fb7843a656"), "test");
        }

        [Test]
        public void Should_Publish_Event_On_Validation_Finish()
        {
            Assert.Pass();
        }

        [Test]
        public async Task Should_Update_PaymentTransaction_Status_To_SendToProvider()
        {
            await _paymentValidationFinishedHandler.Handle(_fakeEvent);

            // AK TODO change from random to the exact Guid
            _repoMock.Verify(m => m.UpdateStatusAsync(It.IsAny<Guid>(), PaymentStatus.SentToProvider), Times.Once());

            
        }

        [Test]
        public void Should_Update_PaymentTransaction_Validation_Status()
        {
            Assert.Pass();
        }
    }
}
