namespace BankProxy.API.ValueObjects
{
    public class CardNumber
    {
        // AK TODO use library to create a valueobject (ValueOf)
        public string Number { get; set; }

        /// <summary>
        /// 6 first digits
        /// </summary>
        public string BankDigits => Number[..5];

        public CardNumber(string number)
        {
            // AK TODO validate and throw
            Number = number;
        }
    }
}
