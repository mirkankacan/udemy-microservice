using Mapster;
using UdemyMicroservice.Payment.Api.Features.Payments.Dtos;

namespace UdemyMicroservice.Payment.Api.Mappings
{
    public class PaymentMappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Features.Payments.Payment, PaymentDto>()
                 .Map(dest => dest.Id, src => src.Id)
                 .Map(dest => dest.OrderCode, src => src.OrderCode)
                 .Map(dest => dest.Amount, src => src.Amount)
                 .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                 .Map(dest => dest.Status, src => src.Status);
        }
    }
}