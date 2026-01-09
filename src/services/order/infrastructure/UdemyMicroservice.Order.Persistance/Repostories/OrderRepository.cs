using Microsoft.EntityFrameworkCore;
using UdemyMicroservice.Order.Application.Contracts.Repositories;
using UdemyMicroservice.Order.Persistance.Data;

namespace UdemyMicroservice.Order.Persistance.Repositories
{
    public class OrderRepository(AppDbContext appDbContext) : GenericRepository<Guid, Domain.Entities.Order>(appDbContext), IOrderRepository
    {
        public async Task<IEnumerable<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId, CancellationToken cancellationToken)
        {
            return await appDbContext.Orders
                .AsNoTracking()
                .Include(x => x.OrderItems)
                .Include(x => x.Address)
                .Where(x => x.BuyerId == buyerId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}