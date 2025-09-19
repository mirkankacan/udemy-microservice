using Mapster;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.Update
{
    public class UpdateCategoryCommandHandler(AppDbContext appDbContext) : IRequestHandler<UpdateCategoryCommand, ServiceResult<UpdateCategoryCommandResponse>>
    {
        public async Task<ServiceResult<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var hasCategory = await appDbContext.Categories.AsNoTracking().AnyAsync(x => x.Id == command.Id, cancellationToken);
            if (hasCategory is false)
            {
                return ServiceResult<UpdateCategoryCommandResponse>.Error("Category not found", $"The category with ID {command.Id} was not found", HttpStatusCode.NotFound);
            }

            var mappedCategory = command.Adapt<Category>();
            appDbContext.Categories.Update(mappedCategory);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<UpdateCategoryCommandResponse>.SuccessAsOk(new UpdateCategoryCommandResponse(mappedCategory.Id));
        }
    }
}