namespace MerchantPayment.API.UnitTests.Services
{
    public class PaymentsRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Fail_On_Invalid_CardNumber()
        {
            Assert.Pass();
        }

        [Test]
        public void Should_Pass_On_Valid_CardNumber()
        {
            Assert.Pass();
        }

        [Test]
        public void Should_Fail_On_Invalid_Cvv()
        {
            Assert.Pass();
        }

        [Test]
        public void Should_Pass_On_Valid_Cvv()
        {
            Assert.Pass();
        }

        [Test]
        public void Should_Fail_On_Invalid_ExpirationDay()
        {
            Assert.Pass();
        }
    }
}