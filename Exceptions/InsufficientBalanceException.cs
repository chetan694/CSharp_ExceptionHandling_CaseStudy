namespace BankingSystem.Exceptions
{
    /// <summary>
    /// Thrown when a withdrawal would bring the balance below the minimum required balance,
    /// or when the requested withdrawal amount exceeds the available balance.
    /// </summary>
    public class InsufficientBalanceException : Exception
    {
        public double CurrentBalance { get; }
        public double AttemptedAmount { get; }

        public InsufficientBalanceException(string message)
            : base(message) { }

        public InsufficientBalanceException(string message, double currentBalance, double attemptedAmount)
            : base(message)
        {
            CurrentBalance = currentBalance;
            AttemptedAmount = attemptedAmount;
        }

        public InsufficientBalanceException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}