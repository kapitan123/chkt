namespace BankProxy.API.UnitTest.IntegrationEvents.EventHandlers;

public class PaymentStatusChangedToReadyForExternalTransactionHandlerTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Should_Prevent_Duplication_Message()
    {
        //var bankMock = new Mock<IBank>();
        //bankMock.Setup(b => b.IsIssuerOf(_defaultCardNumber)).Returns(true);
        //var factory = new BankFactory(new List<IBank>() { bankMock.Object });

        //var bank = factory.GetBankByCardNumber(_defaultCardNumber);

        //bank.Should().NotBeNull();
    }

    [Test]
    public void Should_Emit_PaymentBankTransactionSucceededEvent_On_Bank_Success()
    {

    }

    [Test]
    public void Should_Emit_PaymentBankTransactionFailedEvent_On_Bank_Failure()
    {

    }

    [Test]
    public void Should_Emit_PaymentBankTransactionFailedEvent_On_Exception()
    {

    }
}

