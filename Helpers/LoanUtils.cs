namespace MortgageCalculator.Helpers
{
    public static class LoanUtils
    {
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
    }
}
