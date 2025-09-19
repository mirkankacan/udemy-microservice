using Mapster;
using MediatR;
using System.Net;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.Update
{
    public class UpdateDiscountCommandHandler(AppDbContext appDbContext) : IRequestHandler<UpdateDiscountCommand, ServiceResult<UpdateDiscountCommandResponse>>
    {
        public async Task<ServiceResult<UpdateDiscountCommandResponse>> Handle(UpdateDiscountCommand command, CancellationToken cancellationToken)
        {
            var hasDiscount = await appDbContext.Discounts.AsNoTracking().AnyAsync(x => x.Id == command.Id, cancellationToken);
            if (hasDiscount is false)
            {
                return ServiceResult<UpdateDiscountCommandResponse>.Error("Discount not found", $"Discount with id '{command.Id}' not found", HttpStatusCode.NotFound);
            }

            var mappedDiscount = command.Adapt<DiscountEntity>();
            appDbContext.Discounts.Update(mappedDiscount);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<UpdateDiscountCommandResponse>.SuccessAsOk(new UpdateDiscountCommandResponse(mappedDiscount.Id));
        }
    }
}