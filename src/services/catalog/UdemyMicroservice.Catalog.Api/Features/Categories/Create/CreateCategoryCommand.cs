namespace UdemyMicroservice.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name) : IRequestByServiceResult<CreateCategoryCommandResponse>;
}