namespace UdemyMicroservice.Catalog.Api.Features.Categories.Delete
{
    public class DeleteCategoryCommandHandler(AppDbContext appDbContext) : IRequestHandler<DeleteCategoryCommand, ServiceResult<Unit>>
    {
        public async Task<ServiceResult<Unit>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await appDbContext.Categories.FindAsync(command.Id, cancellationToken);
            if (category is null)
            {
                return ServiceResult<Unit>.Error("Category not found", $"The category with ID {command.Id} was not found", System.Net.HttpStatusCode.NotFound);
            }
            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<Unit>.SuccessAsOk(Unit.Value);
        }
    }
}