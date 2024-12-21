namespace BankingSystem.Domain.Repositories
{
    public interface ICustomerRepository: IRepository<Customer>
	{
		Task<Customer?> GetByDocumentNumberAsync(string documentNumber, CancellationToken cancellationToken = default);
		Task<IEnumerable<Customer>> GetCustomerWithActiveLoanAsync(CancellationToken cancellationToken = default);
		Task<bool> ExistsByDocumentNumberAsync(string documentNumber, CancellationToken cancellationToken = default);
	}
}

