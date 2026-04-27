# CSharp_ExceptionHandling_CaseStudy

A console-based Banking Transaction System built in C# as part of the Great Learning Exception Handling Case Study.

---

## Problem Statement

Build a banking application where users can deposit money, withdraw money, and check their balance. The system must enforce business rules using proper exception handling instead of crashing on invalid input.

Business Rules:
- Minimum account balance is 1000
- Deposit amount must be greater than 0
- Withdrawal cannot exceed current balance
- Withdrawal cannot bring balance below 1000

---

## Project Structure

```
CSharp_ExceptionHandling_CaseStudy/
├── Exceptions/
│   ├── InsufficientBalanceException.cs
│   └── InvalidAmountException.cs
├── Models/
│   └── BankAccount.cs
├── Program.cs
└── CSharp_ExceptionHandling_CaseStudy.csproj
```

---

## Exception Types Used

| Exception | Type | When Thrown |
|-----------|------|-------------|
| InsufficientBalanceException | Custom | Withdrawal exceeds balance or breaches minimum balance |
| InvalidAmountException | Custom | Deposit or withdrawal amount is 0 or negative |
| ArgumentException | Built-in | Account holder name is empty or null |
| InvalidOperationException | Built-in | Initial balance is below minimum |

---

## Sample Output

```
Banking Transaction System

Account created successfully.
Account Holder: Chetan Khajuria
Current Balance: 5000

--- Valid Deposit ---
Depositing: 2000
Deposit successful. New balance: 7000
Deposit operation completed.

--- Invalid Deposit (amount = 0) ---
Depositing: 0
InvalidAmountException: Deposit amount 0 is invalid. Amount must be greater than 0.
Deposit operation completed.

--- Invalid Deposit (amount = -500) ---
Depositing: -500
InvalidAmountException: Deposit amount -500 is invalid. Amount must be greater than 0.
Deposit operation completed.

--- Valid Withdrawal ---
Withdrawing: 3000
Withdrawal successful. New balance: 4000
Withdrawal operation completed.

--- Withdrawal Exceeds Balance ---
Withdrawing: 99999
InsufficientBalanceException: Withdrawal of 99999 exceeds current balance of 4000.
Current Balance: 4000
Attempted Amount: 99999
Withdrawal operation completed.

--- Withdrawal Breaches Minimum Balance ---
Withdrawing: 3500
InsufficientBalanceException: Withdrawal of 3500 would leave 500, which is below the minimum balance of 1000.
Current Balance: 4000
Attempted Amount: 3500
Withdrawal operation completed.

--- Invalid Account Creation (empty name) ---
ArgumentException: Account holder name cannot be null or empty.
Account creation block completed.

--- Invalid Account Creation (balance below minimum) ---
InvalidOperationException: Initial balance 500 is below the minimum required balance of 1000.
Account creation block completed.

--- Final Balance ---
Account Holder: Chetan Khajuria
Current Balance: 4000
```

---

## How to Run

Prerequisites: .NET 8 SDK installed

```bash
git clone https://github.com/YOUR_USERNAME/CSharp_ExceptionHandling_CaseStudy.git
cd CSharp_ExceptionHandling_CaseStudy
dotnet run
```

---

## Concepts Demonstrated

- try-catch-finally blocks with multiple catch handlers
- Custom exceptions with extra properties for detailed error info
- Built-in exceptions for parameter and state validation
- Specific exceptions caught before general ones
- finally block runs regardless of success or failure

---

## Author

Chetan Khajuria — Great Learning C# Exception Handling Case Study
