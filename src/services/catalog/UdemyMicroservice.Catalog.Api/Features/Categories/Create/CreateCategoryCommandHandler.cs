using Mapster;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext appDbContext) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryCommandResponse>>
    {
        public async Task<ServiceResult<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var hasCategory = await appDbContext.Categories
                .AsNoTracking()
                .AnyAsync(c => c.Name == command.Name, cancellationToken);
            if (hasCategory is true)
            {
                return ServiceResult<CreateCategoryCommandResponse>.Error("Category name already exist", $"The category name '{command.Name}' already exist", HttpStatusCode.BadRequest);
            }
            var newCategory = command.Adapt<Category>();
            await appDbContext.Categories.AddAsync(newCategory, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryCommandResponse>.SuccessAsCreated(new CreateCategoryCommandResponse(newCategory.Id), $"/api/categories/{newCategory.Id}");
        }
    }
}