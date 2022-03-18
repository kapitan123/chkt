namespace BankProxy.API.ValueObjects
{
    public class CardNumber
    {
        // AK TODO use library to create a valueobject (ValueOf)
        public string Number { get; set; }

        /// <summary>
        /// 4 first digits a space and next 2 digits
        /// </summary>
        public string BankDigits => Number[..6];

        public CardNumber(string number)
        {
            // AK TODO validate and throw
            Number = number;
        }
    }
}
