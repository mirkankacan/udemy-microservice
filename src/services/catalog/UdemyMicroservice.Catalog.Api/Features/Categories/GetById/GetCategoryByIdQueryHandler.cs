using Mapster;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetById
{
    public class GetCategoryByIdQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await appDbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);
            if (category is null)
            {
                return ServiceResult<CategoryDto>.Error("Category not found", $"The category with ID {query.Id} was not found", HttpStatusCode.NotFound);
            }
            var mappedCategory = category.Adapt<CategoryDto>();
            return ServiceResult<CategoryDto>.SuccessAsOk(mappedCategory);
        }
    }
}