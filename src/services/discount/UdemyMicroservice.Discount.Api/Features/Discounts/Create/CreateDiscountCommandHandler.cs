using Mapster;

namespace UdemyMicroservice.Discount.Api.Features.Discounts.Create
{
    public class CreateDiscountCommandHandler(AppDbContext appDbContext) : IRequestHandler<CreateDiscountCommand, ServiceResult<CreateDiscountCommandResponse>>
    {
        public async Task<ServiceResult<CreateDiscountCommandResponse>> Handle(CreateDiscountCommand command, CancellationToken cancellationToken)
        {
            var hasDiscountForUser = await appDbContext.Discounts
                .AnyAsync(x => x.UserId == command.UserId && x.Code == command.Code, cancellationToken);
            if (hasDiscountForUser is true)
            {
                return ServiceResult<CreateDiscountCommandResponse>.Error("Discount code already exist for user", $"The discount code '{command.Code}' already exist for user", HttpStatusCode.BadRequest);
            }
            var newDiscount = command.Adapt<Discount>();
            await appDbContext.Discounts.AddAsync(newDiscount, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateDiscountCommandResponse>.SuccessAsCreated(new CreateDiscountCommandResponse(newDiscount.Id), $"/api/discounts/{newDiscount.Id}");
        }
    }
}