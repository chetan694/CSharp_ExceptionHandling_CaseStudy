using BankingSystem.Exceptions;
using BankingSystem.Models;

namespace BankingSystem
{
    class Program
    {
        static void RunDeposit(BankAccount account, double amount)
        {
            try
            {
                Console.WriteLine("Depositing: " + amount);
                account.Deposit(amount);
                Console.WriteLine("Deposit successful. New balance: " + account.Balance);
            }
            catch (InvalidAmountException ex)
            {
                Console.WriteLine("InvalidAmountException: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Deposit operation completed.");
            }
        }

        static void RunWithdrawal(BankAccount account, double amount)
        {
            try
            {
                Console.WriteLine("Withdrawing: " + amount);
                account.Withdraw(amount);
                Console.WriteLine("Withdrawal successful. New balance: " + account.Balance);
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine("InsufficientBalanceException: " + ex.Message);
                Console.WriteLine("Current Balance: " + ex.CurrentBalance);
                Console.WriteLine("Attempted Amount: " + ex.AttemptedAmount);
            }
            catch (InvalidAmountException ex)
            {
                Console.WriteLine("InvalidAmountException: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Withdrawal operation completed.");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Banking Transaction System");
            Console.WriteLine();

            BankAccount? account = null;

            try
            {
                account = new BankAccount("Chetan Khajuria", 5000.00);
                Console.WriteLine("Account created successfully.");
                account.CheckBalance();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentException: " + ex.Message);
                return;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("InvalidOperationException: " + ex.Message);
                return;
            }

            Console.WriteLine();
            Console.WriteLine("--- Valid Deposit ---");
            RunDeposit(account, 2000);

            Console.WriteLine();
            Console.WriteLine("--- Invalid Deposit (amount = 0) ---");
            RunDeposit(account, 0);

            Console.WriteLine();
            Console.WriteLine("--- Invalid Deposit (amount = -500) ---");
            RunDeposit(account, -500);

            Console.WriteLine();
            Console.WriteLine("--- Valid Withdrawal ---");
            RunWithdrawal(account, 3000);

            Console.WriteLine();
            Console.WriteLine("--- Withdrawal Exceeds Balance ---");
            RunWithdrawal(account, 99999);

            Console.WriteLine();
            Console.WriteLine("--- Withdrawal Breaches Minimum Balance ---");
            RunWithdrawal(account, 3500);

            Console.WriteLine();
            Console.WriteLine("--- Invalid Account Creation (empty name) ---");
            try
            {
                var bad = new BankAccount("", 5000);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentException: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Account creation block completed.");
            }

            Console.WriteLine();
            Console.WriteLine("--- Invalid Account Creation (balance below minimum) ---");
            try
            {
                var bad = new BankAccount("Test User", 500);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("InvalidOperationException: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Account creation block completed.");
            }

            Console.WriteLine();
            Console.WriteLine("--- Final Balance ---");
            account.CheckBalance();
        }
    }
}