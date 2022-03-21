namespace MerchantPayment.API.UnitTests.Services
{
    public class ValidationServiceTests
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

        // AK TODO There are problems with dateTime.Now but a nise place to flex my knowledge
        // makes sense to expose these methods
        [Test]
        public void Should_Fail_On_Invalid_ExpirationDay()
        {
            Assert.Pass();
        }
    }
}