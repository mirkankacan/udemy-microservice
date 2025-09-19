namespace UdemyMicroservice.Catalog.Api.Features.Categories.Delete
{
    public record DeleteCategoryCommand(Guid Id) : IRequestByServiceResult<Unit>;
}