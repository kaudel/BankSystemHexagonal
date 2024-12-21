using BankingSystem.Domain.ValueObjects;

namespace BankingSystem.Domain.Services
{
    public interface ILoanElegibilityService
	{
		Task<LoanElegibilityResult> EvaluateElegibilityAsync(
			Customer customer,
			Money requestedAmount,
			LoanTerm term,
			CancellationToken cancellation = default);
	}
}

