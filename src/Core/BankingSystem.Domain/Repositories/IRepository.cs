namespace BankingSystem.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}

