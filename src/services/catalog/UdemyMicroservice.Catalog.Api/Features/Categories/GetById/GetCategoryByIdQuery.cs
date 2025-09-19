namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;
}