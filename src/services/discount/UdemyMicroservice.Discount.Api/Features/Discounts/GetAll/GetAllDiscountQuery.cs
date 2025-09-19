using UdemyMicroservice.Discount.Api.Features.Discounts.Dtos;
using UdemyMicroservice.Shared;

namespace UdemyMicroservice.Catalog.Api.Features.Categories.GetAll
{
    public record GetAllDiscountQuery : IRequestByServiceResult<IEnumerable<DiscountDto>>;
}