namespace MerchantPayment.API.UnitTests.Services;

public class RequestValidationServiceTests
{
    private readonly DateTime _defaultNow = DateTime.Parse("2022-08-01T00:00:00-07:00");
    private SubmitPaymentRequest _defaultReq;
    private Mock<ISystemClock> _clockMock;

    [SetUp]
    public void Setup()
    {
        _clockMock = new Mock<ISystemClock>();
        _clockMock.Setup(c => c.UtcNow).Returns(new DateTimeOffset(_defaultNow));

        _defaultReq = new SubmitPaymentRequest
        {
            CardDetails = new("5555555555554444", "Test tester", DateTime.Parse("2023-08-01T00:00:00-07:00"), "228"),
            Sum = new(100, "USD"),
            Message = "test"
        };
    }

    [Test]
    public void Should_Pass_On_Valid_CardNumber()
    {
        var vs = new RequestValidationService(_clockMock.Object);

        var result = vs.ValidateSubmitPaymentRequest(_defaultReq);

        result.Errors.Should().HaveCount(0);
    }

    [Test]
    public void Should_Fail_On_Invalid_CardNumber()
    {
        var vs = new RequestValidationService(_clockMock.Object);

        _defaultReq.CardDetails = _defaultReq.CardDetails with { Number = "5k255555555554444"};

        var result = vs.ValidateSubmitPaymentRequest(_defaultReq);

        result.Errors.Should().HaveCount(1);
    }

    [Test]
    [TestCase(-100)]
    [TestCase(0)]
    public void Should_Fail_On_Less_Than_Zero_Summ(int amount)
    {
        var vs = new RequestValidationService(_clockMock.Object);

        _defaultReq.Sum = _defaultReq.Sum with { Amount = amount };

        var result = vs.ValidateSubmitPaymentRequest(_defaultReq);

        result.Errors.Should().HaveCount(1);
    }

    [Test]
    public void Should_Fail_On_Invalid_ExpirationDay()
    {
        var vs = new RequestValidationService(_clockMock.Object);

        _defaultReq.CardDetails = _defaultReq.CardDetails with { Expiration = DateTime.Parse("2020-08-01T00:00:00-07:00") };

        var result = vs.ValidateSubmitPaymentRequest(_defaultReq);

        result.Errors.Should().HaveCount(1);
    }
}