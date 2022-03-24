namespace BankProxy.API.UnitTest.Services;

public class BankFactoryTests
{
    private readonly CardNumber _defaultCardNumber = new("5555555555554444");

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Should_Return_IssuerBank_Based_On_Card_Number()
    {
        var bankMock = new Mock<IBank>();
            bankMock.Setup(b => b.IsIssuerOf(_defaultCardNumber)).Returns(true);
        var factory = new BankFactory(new List<IBank>() { bankMock.Object });

        var bank = factory.GetBankByCardNumber(_defaultCardNumber);

        bank.Should().NotBeNull();
    }

    [Test]
    public void Should_Throw_When_No_Issuer_Was_Found()
    {
        var bankMock = new Mock<IBank>();
        bankMock.Setup(b => b.IsIssuerOf(_defaultCardNumber)).Returns(false);
        var factory = new BankFactory(new List<IBank>() { bankMock.Object });

        var act = () => factory.GetBankByCardNumber(_defaultCardNumber);

        act.Should().Throw<ArgumentException>();
    }
}
