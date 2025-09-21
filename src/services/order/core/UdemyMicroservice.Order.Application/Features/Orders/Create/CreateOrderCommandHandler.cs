using MediatR;
using System.Net;
using UdemyMicroservice.Order.Application.Contracts;
using UdemyMicroservice.Order.Application.Contracts.Repositories;
using UdemyMicroservice.Order.Application.Contracts.UnitOfWork;
using UdemyMicroservice.Order.Domain.Entities;
using UdemyMicroservice.Shared;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, IGenericRepository<int, Address> adressRepository, IIdentityService identityService, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, ServiceResult<Unit>>
    {
        public async Task<ServiceResult<Unit>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            if (!command.Items.Any())
            {
                return ServiceResult<Unit>.Error("No order items found.", HttpStatusCode.BadRequest);
            }
            var newAddress = new Address
            {
                Province = command.Address.Province,
                District = command.Address.District,
                Street = command.Address.Street,
                ZipCode = command.Address.ZipCode,
                Line = command.Address.Line
            };
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            var order = Domain.Entities.Order.CreateUnpaidOrder(identityService.GetUserId, command.DiscountRate, newAddress.Id);

            foreach (var item in command.Items)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price);
            }
            order.Address = newAddress;
            await orderRepository.AddAsync(order, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            var paymentId = Guid.Empty;

            order.SetPaidStatus(paymentId);

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return ServiceResult<Unit>.SuccessAsCreated(Unit.Value, "");
        }
    }
}