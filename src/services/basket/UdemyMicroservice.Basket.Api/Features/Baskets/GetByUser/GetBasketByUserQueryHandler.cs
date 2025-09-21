using Mapster;
using UdemyMicroservice.Basket.Api.Features.Baskets.Dtos;
using UdemyMicroservice.Basket.Api.Services;

namespace UdemyMicroservice.Basket.Api.Features.Baskets.GetByUser
{
    public class GetBasketByUserQueryHandler(IBasketService basketService) : IRequestHandler<GetBasketByUserQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketByUserQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasketFromCacheAsync(cancellationToken);
            if (basket is null || !basket.Items.Any())
            {
                basket = basketService.CreateBasket();
            }
            var mappedBasket = basket.Adapt<BasketDto>();
            return ServiceResult<BasketDto>.SuccessAsOk(mappedBasket);
        }
    }
}