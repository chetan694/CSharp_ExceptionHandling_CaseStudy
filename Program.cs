using BankingSystem.Exceptions;
using BankingSystem.Models;

namespace BankingSystem
{
    class Program
    {
        // ── Helpers ───────────────────────────────────────────────────────────
        static void PrintHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine($"  {title}");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
        }

        static void PrintSuccess(string message) =>
            Console.WriteLine($"  ✔  {message}");

        static void PrintError(string label, string message) =>
            Console.WriteLine($"  ✘  [{label}] {message}");

        // ── Scenario Runners ──────────────────────────────────────────────────

        /// <summary>US1 – Valid deposit</summary>
        static void RunDeposit(BankAccount account, double amount)
        {
            try
            {
                Console.WriteLine($"\n  → Depositing ₹{amount:F2} ...");
                account.Deposit(amount);
                PrintSuccess($"Deposit successful! New balance: ₹{account.Balance:F2}");
            }
            catch (InvalidAmountException ex)
            {
                PrintError("InvalidAmountException", ex.Message);
            }
            catch (Exception ex)
            {
                PrintError("UnexpectedException", ex.Message);
            }
            finally
            {
                Console.WriteLine("  [Finally] Deposit operation completed.");
            }
        }

        /// <summary>US2 / US3 – Valid & invalid withdrawals</summary>
        static void RunWithdrawal(BankAccount account, double amount)
        {
            try
            {
                Console.WriteLine($"\n  → Withdrawing ₹{amount:F2} ...");
                account.Withdraw(amount);
                PrintSuccess($"Withdrawal successful! New balance: ₹{account.Balance:F2}");
            }
            catch (InsufficientBalanceException ex)
            {
                PrintError("InsufficientBalanceException", ex.Message);
                Console.WriteLine($"     Current Balance  : ₹{ex.CurrentBalance:F2}");
                Console.WriteLine($"     Attempted Amount : ₹{ex.AttemptedAmount:F2}");
            }
            catch (InvalidAmountException ex)
            {
                PrintError("InvalidAmountException", ex.Message);
            }
            catch (Exception ex)
            {
                PrintError("UnexpectedException", ex.Message);
            }
            finally
            {
                Console.WriteLine("  [Finally] Withdrawal operation completed.");
            }
        }

        /// <summary>Shows how ArgumentException / InvalidOperationException surface at construction.</summary>
        static void RunInvalidAccountCreation()
        {
            PrintHeader("SCENARIO: Invalid Account Creation");

            // Blank name
            try
            {
                Console.WriteLine("\n  → Creating account with empty name ...");
                var bad = new BankAccount("", 5000);
            }
            catch (ArgumentException ex)
            {
                PrintError("ArgumentException", ex.Message);
            }
            finally
            {
                Console.WriteLine("  [Finally] Account-creation block (empty name) completed.");
            }

            // Initial balance below minimum
            try
            {
                Console.WriteLine("\n  → Creating account with ₹500 initial balance ...");
                var bad = new BankAccount("Raj Kumar", 500);
            }
            catch (InvalidOperationException ex)
            {
                PrintError("InvalidOperationException", ex.Message);
            }
            finally
            {
                Console.WriteLine("  [Finally] Account-creation block (low balance) completed.");
            }
        }

        // ── Entry Point ───────────────────────────────────────────────────────
        static void Main(string[] args)
        {
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║     BANKING TRANSACTION SYSTEM — C# Demo         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");

            // ─── Create a valid account ───────────────────────────────────────
            BankAccount? account = null;
            try
            {
                account = new BankAccount("Priya Sharma", 5000.00);
                Console.WriteLine("\n  Account created successfully.");
                Console.Write("  Initial ");
                account.CheckBalance();
            }
            catch (ArgumentException ex)
            {
                PrintError("ArgumentException", ex.Message);
                return;
            }
            catch (InvalidOperationException ex)
            {
                PrintError("InvalidOperationException", ex.Message);
                return;
            }

            // ─── US1: Valid deposit ───────────────────────────────────────────
            PrintHeader("US1 – Valid Deposit");
            RunDeposit(account, 2000);

            // ─── US4: Invalid deposit (zero) ──────────────────────────────────
            PrintHeader("US4 – Invalid Deposit (amount = 0)");
            RunDeposit(account, 0);

            // ─── US4: Invalid deposit (negative) ─────────────────────────────
            PrintHeader("US4 – Invalid Deposit (amount = -500)");
            RunDeposit(account, -500);

            // ─── US2: Valid withdrawal ────────────────────────────────────────
            PrintHeader("US2 – Valid Withdrawal");
            RunWithdrawal(account, 3000);

            // ─── US3: Withdrawal exceeds balance ─────────────────────────────
            PrintHeader("US3 – Withdrawal Exceeds Balance");
            RunWithdrawal(account, 99999);

            // ─── US3: Withdrawal breaches minimum balance ─────────────────────
            PrintHeader("US3 – Withdrawal Would Breach Minimum Balance (₹1000)");
            // Current balance after above ops: 5000 + 2000 - 3000 = 4000
            // Trying to withdraw 3500 would leave ₹500 < ₹1000
            RunWithdrawal(account, 3500);

            // ─── US5: Custom exception details visible ────────────────────────
            PrintHeader("US5 – Custom Exceptions Demonstrated (Balance Check)");
            Console.WriteLine("\n  Final account state:");
            account.CheckBalance();

            // ─── Built-in exceptions during construction ──────────────────────
            RunInvalidAccountCreation();

            Console.WriteLine();
            Console.WriteLine("══════════════════════════════════════════════════");
            Console.WriteLine("  All scenarios executed. Program exited cleanly.");
            Console.WriteLine("══════════════════════════════════════════════════");
        }
    }
}