using Mapster;
using UdemyMicroservice.Order.Application.Dtos;

namespace UdemyMicroservice.Order.Application.Mappings
{
    public class OrderMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Order -> OrderResultDto
            config.NewConfig<Domain.Entities.Order, OrderDto>()
                 .Map(dest => dest.OrderDate, src => src.CreatedAt)
                 .Map(dest => dest.TotalPrice, src => src.LastPrice)
                 .Map(dest => dest.Items, src => src.OrderItems)
                 .Map(dest => dest.Address, src => src.Address);
        }
    }
}