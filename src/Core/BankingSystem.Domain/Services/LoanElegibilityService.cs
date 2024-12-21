using BankingSystem.Domain.Enum;
using BankingSystem.Domain.ValueObjects;

namespace BankingSystem.Domain.Services
{
    public class LoanElegibilityService:ILoanElegibilityService
	{
        private const decimal MAX_DEBT_TO_INCOME_RATIO = 0.4m;
        private const int MIN_CREDIT_SCORE = 600;
        private const int MINIMUM_EMPLOYMENT_MONTHS = 6;


		public LoanElegibilityService()
		{
		}

        public async Task<LoanElegibilityResult> EvaluateElegibilityAsync(
            Customer customer,
            Money requestedAmount,
            LoanTerm term,
            CancellationToken cancellationToken = default)
        {
            var rejectedReasons = new List<string>();

            if (customer.Status != CustomerStatus.Active)
                rejectedReasons.Add("Customer is not active");

            if (customer.Loans.Any(x => x.Status == LoanStatus.Active))
                rejectedReasons.Add("Customer already has an active loan");

            if (requestedAmount.Amount < 1000 || requestedAmount.Amount > 50000)
                rejectedReasons.Add("Requested amount must be between 1000 and 50000");

            if (rejectedReasons.Any())
                return LoanElegibilityResult.Rejected(rejectedReasons);

            return LoanElegibilityResult.Approved();
        }
    }
}

