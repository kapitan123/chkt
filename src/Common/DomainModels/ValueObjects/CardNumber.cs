namespace BankProxy.API.ValueObjects
{
    public class CardNumber
    {
        public string Number { get; set; }

        /// <summary>
        /// 6 first digits
        /// </summary>
        public string BankDigits => Number[..5];

        public CardNumber(string number)
        {
            Number = number;
        }
    }
}
