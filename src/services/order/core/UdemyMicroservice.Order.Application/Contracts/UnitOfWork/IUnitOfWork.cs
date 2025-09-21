namespace UdemyMicroservice.Order.Application.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        bool HasActiveTransaction { get; }
    }
}