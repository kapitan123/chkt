namespace MerchantPayment.API.UnitTests.Services;

public class PaymentsRepositoryTests
{
    private readonly DateTime _defaultNow = DateTime.Parse("2022-08-01T00:00:00-07:00");
    private readonly PaymentAmount _amount = new(100, "USD");
    private readonly CardDetails _cardDetails = new ("5555555555554444", "Test tester", DateTime.Parse("2023-08-01T00:00:00-07:00"), "228");

    private Mock<DaprClient> _daprMock;
    private Mock<ILogger<PaymentsRepository>> _loggerMock;
    private Mock<ISystemClock> _clockMock;

    [SetUp]
    public void Setup()
    {
        _clockMock = new Mock<ISystemClock>();
        _clockMock.Setup(c => c.UtcNow).Returns(new DateTimeOffset(_defaultNow));

        _loggerMock = new Mock<ILogger<PaymentsRepository>>();
        _daprMock = new Mock<DaprClient>();
    }   

    [Test]
    public async Task Should_Masked_Card_Number_On_Save()
    {
        var pr = new PaymentsRepository(_daprMock.Object, _loggerMock.Object, _clockMock.Object);

        await pr.CreatePaymentAsync(_amount, _cardDetails, "test");

        _daprMock.Verify(d => d.SaveStateAsync(
            It.IsAny<string>(), 
            It.IsAny<string>(),
            It.Is<PaymentTransaction>(p => p.CardDetails.Number == "555******54444"),
            null,
            null,
            new CancellationToken(false)),
            Times.Once);
    }

    [Test]
    public async Task  Should_Save_In_State_Created()
    {
        var pr = new PaymentsRepository(_daprMock.Object, _loggerMock.Object, _clockMock.Object);

        await pr.CreatePaymentAsync(_amount, _cardDetails, "test");

        _daprMock.Verify(d => d.SaveStateAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.Is<PaymentTransaction>(p => p.Status == PaymentStatus.Created),
            null,
            null,
            new CancellationToken(false)),
            Times.Once);
    }
}