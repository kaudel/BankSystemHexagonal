using BankingSystem.Domain.Enum;

namespace BankingSystem.Domain.Repositories
{
    public interface ILoanRepository: IRepository<Loan>
	{
		Task<IEnumerable<Loan>> GetCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
		Task<IEnumerable<Loan>> GetByStatusAsync(LoanStatus loanStatus, CancellationToken cancellationToken = default);
		Task<decimal> GetTotalLoanAmountByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
		Task<bool> ExistsActiveLoanByCustomer(Guid customerId, CancellationToken cancellationToken = default);
	}
}

