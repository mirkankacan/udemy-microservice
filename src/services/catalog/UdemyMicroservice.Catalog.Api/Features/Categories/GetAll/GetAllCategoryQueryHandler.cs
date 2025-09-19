using Mapster;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryQueryHandler(AppDbContext appDbContext) : IRequestHandler<GetAllCategoryQuery, ServiceResult<IEnumerable<CategoryDto>>>
    {
        public async Task<ServiceResult<IEnumerable<CategoryDto>>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            var categories = await appDbContext.Categories.AsNoTracking().ToListAsync(cancellationToken);
            if (categories is null)
            {
                return ServiceResult<IEnumerable<CategoryDto>>.Error("No categories found", "There are no categories available", HttpStatusCode.NotFound);
            }

            var mappedCategories = categories.Adapt<IEnumerable<CategoryDto>>();

            return ServiceResult<IEnumerable<CategoryDto>>.SuccessAsOk(mappedCategories);
        }
    }
}