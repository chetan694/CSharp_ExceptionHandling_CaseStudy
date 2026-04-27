using BankingSystem.Exceptions;

namespace BankingSystem.Models
{
    /// <summary>
    /// Represents a bank account with deposit, withdrawal, and balance check operations.
    /// Business Rules:
    ///   - Minimum balance: ₹1000
    ///   - Deposit amount must be > 0
    ///   - Withdrawal cannot exceed balance or cause balance to drop below ₹1000
    /// </summary>
    public class BankAccount
    {
        // ── Constants ──────────────────────────────────────────────────────────
        public const double MinimumBalance = 1000.0;

        // ── Properties ────────────────────────────────────────────────────────
        public string AccountHolderName { get; private set; }
        public double Balance { get; private set; }

        // ── Constructor ───────────────────────────────────────────────────────
        /// <summary>
        /// Initialises a new BankAccount.
        /// </summary>
        /// <param name="accountHolderName">Name of the account holder.</param>
        /// <param name="initialBalance">Opening balance (must be ≥ MinimumBalance).</param>
        /// <exception cref="ArgumentException">Thrown when the holder name is null/empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when initial balance is below minimum.</exception>
        public BankAccount(string accountHolderName, double initialBalance)
        {
            if (string.IsNullOrWhiteSpace(accountHolderName))
                throw new ArgumentException("Account holder name cannot be null or empty.", nameof(accountHolderName));

            if (initialBalance < MinimumBalance)
                throw new InvalidOperationException(
                    $"Initial balance ₹{initialBalance:F2} is below the minimum required balance of ₹{MinimumBalance:F2}.");

            AccountHolderName = accountHolderName.Trim();
            Balance = initialBalance;
        }

        // ── Public Methods ────────────────────────────────────────────────────

        /// <summary>
        /// Deposits the specified amount into the account.
        /// </summary>
        /// <param name="amount">Amount to deposit (must be > 0).</param>
        /// <exception cref="InvalidAmountException">Thrown when amount is ≤ 0.</exception>
        public void Deposit(double amount)
        {
            // Validate amount
            if (amount <= 0)
                throw new InvalidAmountException(
                    $"Deposit amount ₹{amount:F2} is invalid. Amount must be greater than ₹0.", amount);

            Balance += amount;
        }

        /// <summary>
        /// Withdraws the specified amount from the account.
        /// </summary>
        /// <param name="amount">Amount to withdraw (must be > 0 and keep balance ≥ MinimumBalance).</param>
        /// <exception cref="InvalidAmountException">Thrown when amount is ≤ 0.</exception>
        /// <exception cref="InsufficientBalanceException">
        ///     Thrown when amount exceeds balance or would breach minimum balance.
        /// </exception>
        public void Withdraw(double amount)
        {
            // Validate amount
            if (amount <= 0)
                throw new InvalidAmountException(
                    $"Withdrawal amount ₹{amount:F2} is invalid. Amount must be greater than ₹0.", amount);

            // Check against current balance
            if (amount > Balance)
                throw new InsufficientBalanceException(
                    $"Withdrawal of ₹{amount:F2} exceeds current balance of ₹{Balance:F2}.",
                    Balance, amount);

            // Enforce minimum balance rule
            double balanceAfterWithdrawal = Balance - amount;
            if (balanceAfterWithdrawal < MinimumBalance)
                throw new InsufficientBalanceException(
                    $"Withdrawal of ₹{amount:F2} would leave ₹{balanceAfterWithdrawal:F2}, " +
                    $"which is below the minimum required balance of ₹{MinimumBalance:F2}.",
                    Balance, amount);

            Balance -= amount;
        }

        /// <summary>
        /// Prints the current account balance to the console.
        /// </summary>
        public void CheckBalance()
        {
            Console.WriteLine($"  Account Holder : {AccountHolderName}");
            Console.WriteLine($"  Current Balance: ₹{Balance:F2}");
        }
    }
}