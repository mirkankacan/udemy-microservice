using Iyzipay.Model;
using Iyzipay.Request;
using MediatR;
using System.Net;
using UdemyMicroservice.Payment.Api.Data;
using UdemyMicroservice.Shared;
using UdemyMicroservice.Shared.Services;

namespace UdemyMicroservice.Payment.Api.Features.Payments.Create
{
    public class CreatePaymentCommandHandler(AppDbContext appDbContext, IIdentityService identityService) : IRequestHandler<CreatePaymentCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(command.CardNumber, command.CardHolderName, command.CardExpirationDate, command.CardCvv);
            if (!isSuccess)
            {
                return ServiceResult<Guid>.Error("Payment failed", errorMessage, HttpStatusCode.BadRequest);
            }
            var userId = identityService.GetUserId;
            var newPayment = new Features.Payments.Payment(userId, command.OrderCode, command.Amount);
            newPayment.SetStatus(PaymentStatus.Success);
            await appDbContext.Payments.AddAsync(newPayment, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<Guid>.SuccessAsCreated(newPayment.Id, "");
        }

        private async Task<(bool isSuccess, string? error)> ExternalPaymentProcessAsync(string cardNumber, string cardHolderName, string cardExpirationDate, string cardCvv)
        {
            CreatePayWithIyzicoInitializeRequest request = new CreatePayWithIyzicoInitializeRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.BasketId = "B67832";
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "http://localhost:5153/payments/callback";

            List<int> enabledInstallments = new List<int>();
            enabledInstallments.Add(2);
            enabledInstallments.Add(3);
            enabledInstallments.Add(6);
            enabledInstallments.Add(9);
            request.EnabledInstallments = enabledInstallments;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            //PayWithIyzicoInitialize payWithIyzicoInitialize = await PayWithIyzicoInitialize.Create(request, options);

            //PrintResponse<PayWithIyzicoInitialize>(payWithIyzicoInitialize);

            //Assert.AreEqual(Status.SUCCESS.ToString(), payWithIyzicoInitialize.Status);
            //Assert.AreEqual(Locale.TR.ToString(), payWithIyzicoInitialize.Locale);
            //Assert.AreEqual("123456789", payWithIyzicoInitialize.ConversationId);
            //Assert.IsNotNull(payWithIyzicoInitialize.SystemTime);
            //Assert.IsNull(payWithIyzicoInitialize.ErrorMessage);
            //Assert.IsNotNull(payWithIyzicoInitialize.CheckoutFormContent);
            //Assert.IsNotNull(payWithIyzicoInitialize.PayWithIyzicoPageUrl);
            await Task.Delay(2000);
            return (true, null);
        }
    }
}