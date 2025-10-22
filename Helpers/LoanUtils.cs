using MortgageCalculator.Models;

namespace MortgageCalculator.Helpers
{
    public static class LoanUtils
    {
        /// <summary>
        /// Calculate the monthly payment schedule for a loan
        /// </summary>
        /// <param name="loan"></param>
        /// <returns>A Loan object</returns>
        public static Loan GetPayments(Loan loan)
        {
            loan.Payments.Clear();

            //Calculate the monthly payment
            loan.Payment = CalcPayment(loan.PurchaseAmount, loan.Rate, loan.Term);

            //Variables to hold the total interest and balance
            double balance = loan.PurchaseAmount;
            double totalInterest = 0;
            double monthlyPrincipal = 0;
            double monthlyInterest = 0;
            double monthlyRate = CalcMonthlyRate(loan.Rate);
            var months = loan.Term * 12;

            for (int month = 1; month <= months; month++)
            {
                monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                loan.Payments.Add(new LoanPayment
                {
                    Month = month,
                    Payment = loan.Payment,
                    MonthlyPrincipal = monthlyPrincipal,
                    MonthlyInterest = monthlyInterest,
                    TotalInterest = totalInterest,
                    Balance = balance < 0 ? 0 : balance,
                });
            }

            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.PurchaseAmount + totalInterest;

            return loan;
        }
        
        /// <summary>
        /// Calculates a monthly payment for a simple interest loan
        /// </summary>
        /// <param name="amount">Loan Amount</param>
        /// <param name="rate">Annualized Rate as a Double</param>
        /// <param name="term">Term in Years</param>
        /// <returns>A monthly payment as a double</returns>
        public static double CalcPayment(double amount, double rate, double term)
        {
            var monthlyRate = CalcMonthlyRate(rate);
            var months = term * 12;
            var payment = (amount * monthlyRate) / (1 - Math.Pow(1 + monthlyRate, -months));
            return payment;
        }

        private static double CalcMonthlyRate(double rate)
        {
            return rate / 1200;
        }

        public static double CalcMonthlyInterest(double balance, double monthlyRate)
        {
            return balance * monthlyRate;
        }
    }
}
