namespace UdemyMicroservice.Discount.Api.Features.Discounts.Delete
{
    public class DeleteDiscountCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteDiscountCommand, ServiceResult<Unit>>
    {
        public async Task<ServiceResult<Unit>> Handle(DeleteDiscountCommand command, CancellationToken cancellationToken)
        {
            var discount = await appDbContext.Discounts.FindAsync(command.Id, cancellationToken);
            if (discount is null)
            {
                return ServiceResult<Unit>.Error("Discount not found", $"The discount with ID {command.Id} was not found", System.Net.HttpStatusCode.NotFound);
            }
            appDbContext.Discounts.Remove(discount);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<Unit>.SuccessAsOk(Unit.Value);
        }
    }
}