using UdemyMicroservice.Basket.Api.Features.Baskets.Dtos;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.GetByUser
{
    public record GetBasketByUserQuery : IRequestByServiceResult<BasketDto>;
}