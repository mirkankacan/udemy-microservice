namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public record GetAllDiscountQuery : IRequestByServiceResult<IEnumerable<DiscountDto>>;
}