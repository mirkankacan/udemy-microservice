namespace UdemyMicroservice.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<Guid, Domain.Entities.Order>
    {
        Task<IEnumerable<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId, CancellationToken cancellationToken);
    }
}