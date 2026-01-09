using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UdemyMicroservice.Order.Application.Contracts;
using UdemyMicroservice.Order.Domain.Entities;
using UdemyMicroservice.Order.Persistance.Data;

namespace UdemyMicroservice.Order.Persistance.Repositories
{
    public class GenericRepository<TId, TEntity>(AppDbContext appDbContext) : IGenericRepository<TId, TEntity> where TId : struct where TEntity : BaseEntity<TId>

    {
        protected AppDbContext Context = appDbContext;
        private readonly DbSet<TEntity> _dbSet = appDbContext.Set<TEntity>();

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task<bool> AnyAsync(TId id, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _dbSet.AnyAsync(predicate, cancellationToken);
        }

        public async Task Remove(TId id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
        {
            return _dbSet.FindAsync(id, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}