using System.Linq.Expressions;
using UdemyMicroservice.Order.Domain.Entities;

namespace UdemyMicroservice.Order.Application.Contracts
{
    public interface IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId>
    {
        Task<bool> AnyAsync(TId id, CancellationToken cancellationToken);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);

        Task<TEntity?> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        void Update(TEntity entity);

        Task Remove(TId id, CancellationToken cancellationToken);
    }
}