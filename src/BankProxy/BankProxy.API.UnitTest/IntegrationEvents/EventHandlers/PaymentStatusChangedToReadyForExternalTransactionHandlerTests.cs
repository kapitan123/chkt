using BankProxy.API.Models;
using BankProxy.API.Models.DTO;

namespace BankProxy.API.UnitTest.IntegrationEvents.EventHandlers;

public class PaymentStatusChangedToReadyForExternalTransactionHandlerTests
{
    private Mock<ILogger<PaymentStatusChangedToReadyForExternalTransactionHandler>> _logger;
    private Mock<IBankFactory> _bankProvidersFactory;
    private Mock<IProcessedMessagesRepository> _messagesRepo;
    private Mock<IEventBus> _eventBusMock;

    private readonly PaymentAmount _amount = new(100, "USD");
    private readonly CardDetails _cardDetails = new("5555555555554444", "Test tester", DateTime.Parse("2023-08-01T00:00:00-07:00"), "228");
    private readonly Guid paymentId = Guid.Parse("34f25424-088c-482a-a75e-8ccbbecf8112");

    [SetUp]
    public void Setup()
    {
        _eventBusMock = new Mock<IEventBus>();
        _messagesRepo = new Mock<IProcessedMessagesRepository>();

        _bankProvidersFactory = new Mock<IBankFactory>();
        
        _logger = new Mock<ILogger<PaymentStatusChangedToReadyForExternalTransactionHandler>>();
    }

    [Test]
    public async Task Should_Prevent_Duplication_Event_Processing()
    {
        _messagesRepo.Setup(mr => mr.IsMessageDuplicate(It.IsAny<Guid>())).ReturnsAsync(true);

        var handler = new PaymentStatusChangedToReadyForExternalTransactionHandler(
            _logger.Object,
            _bankProvidersFactory.Object,
            _messagesRepo.Object,
            _eventBusMock.Object
            );

        var testEvent = new PaymentStatusChangedToReadyForExternalTransactionEvent(
            paymentId, _cardDetails, _amount, "test");

        await handler.Handle(testEvent);
        await handler.Handle(testEvent);

        _messagesRepo.Verify(mr => mr.SavePendingMessage(
            It.IsAny<PaymentStatusChangedToReadyForExternalTransactionEvent>()),
                Times.Never);
    }

    [Test]
    public async Task Should_Emit_PaymentBankTransactionSucceededEvent_On_Bank_Success()
    {
        var bankRef = "BankReference";
        var fakebankResult = new BankResponse(1, "BankMessage", true, bankRef);
        var fakeBank = new Mock<IBank>();
        fakeBank.Setup(fb => fb.ProcessTransaction(It.IsAny<BankPayment>())).ReturnsAsync(fakebankResult);

        _bankProvidersFactory.Setup(
            bf => bf.GetBankByCardNumber(It.IsAny<CardNumber>()))
            .Returns(fakeBank.Object);

        var handler = new PaymentStatusChangedToReadyForExternalTransactionHandler(
            _logger.Object,
            _bankProvidersFactory.Object,
            _messagesRepo.Object,
            _eventBusMock.Object
            );

        var testEvent = new PaymentStatusChangedToReadyForExternalTransactionEvent(
            paymentId, _cardDetails, _amount, "test");

        await handler.Handle(testEvent);

        _eventBusMock.Verify(mr => mr.PublishAsync(
            It.Is<PaymentBankTransactionSucceededEvent>(e => e.PaymentId == paymentId && e.BankReference == bankRef)),
                Times.Once);
    }

    [Test]
    public async Task Should_Emit_PaymentBankTransactionFailedEvent_On_Bank_Failure()
    {
        var failReason = "FailReason";
        var fakebankResult = new BankResponse(1, failReason, false, "BankReference");
        var fakeBank = new Mock<IBank>();
        fakeBank.Setup(fb => fb.ProcessTransaction(It.IsAny<BankPayment>())).ReturnsAsync(fakebankResult);

        _bankProvidersFactory.Setup(
            bf => bf.GetBankByCardNumber(It.IsAny<CardNumber>()))
            .Returns(fakeBank.Object);

        var handler = new PaymentStatusChangedToReadyForExternalTransactionHandler(
         _logger.Object,
         _bankProvidersFactory.Object,
         _messagesRepo.Object,
         _eventBusMock.Object
         );

        var testEvent = new PaymentStatusChangedToReadyForExternalTransactionEvent(
            paymentId, _cardDetails, _amount, "test");

        await handler.Handle(testEvent);

        _eventBusMock.Verify(mr => mr.PublishAsync(
            It.Is<PaymentBankTransactionFailedEvent>(e => e.PaymentId == paymentId && e.Reason == failReason)),
                Times.Once);
    }

    [Test]
    public async Task Should_Emit_PaymentBankTransactionFailedEvent_On_Exception()
    {
        var error = "testError";
        var fakeBank = new Mock<IBank>();
        fakeBank.Setup(fb => fb.ProcessTransaction(It.IsAny<BankPayment>()))
            .ThrowsAsync(new Exception("testError"));

        _bankProvidersFactory.Setup(
            bf => bf.GetBankByCardNumber(It.IsAny<CardNumber>()))
            .Returns(fakeBank.Object);

        var handler = new PaymentStatusChangedToReadyForExternalTransactionHandler(
         _logger.Object,
         _bankProvidersFactory.Object,
         _messagesRepo.Object,
         _eventBusMock.Object
         );

        var testEvent = new PaymentStatusChangedToReadyForExternalTransactionEvent(
            paymentId, _cardDetails, _amount, "test");

        await handler.Handle(testEvent);

        _eventBusMock.Verify(mr => mr.PublishAsync(
            It.Is<PaymentBankTransactionFailedEvent>(e => e.PaymentId == paymentId && e.Reason == error)),
                Times.Once);
    }
}

