namespace BankingSystem.Exceptions
{
    /// <summary>
    /// Thrown when a deposit or withdrawal amount is invalid
    /// (e.g., zero, negative, or non-numeric input).
    /// </summary>
    public class InvalidAmountException : Exception
    {
        public double AttemptedAmount { get; }

        public InvalidAmountException(string message)
            : base(message) { }

        public InvalidAmountException(string message, double attemptedAmount)
            : base(message)
        {
            AttemptedAmount = attemptedAmount;
        }

        public InvalidAmountException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}