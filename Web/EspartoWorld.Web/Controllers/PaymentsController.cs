namespace EspartoWorld.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Stripe;
    using Stripe.Checkout;

    public class PaymentsController : Controller
    {
        private readonly IShoppingCartsService shoppingCartsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductsService productsService;

        public PaymentsController(IShoppingCartsService shoppingCartsService, UserManager<ApplicationUser> userManager, IProductsService productsService)
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "sk_test_51HrjU3AlRqb1byiWbWHssz4je4QlVU4Rtb4jCrJSsDNHUuHuEmrA6AL4A7yaVNF1BEP0e6b8jKsvXaoVjK8ASjMX00b34KEIVK";
            this.shoppingCartsService = shoppingCartsService;
            this.userManager = userManager;
            this.productsService = productsService;
        }

        public async Task<IActionResult> SuccessAsync(string session_id)
        {
            // TO DO get status of payment if is paid this logic
            if (session_id != null)
            {
                var userId = this.userManager.GetUserId(this.User);
                var items = this.shoppingCartsService.GetAllByUserId<ShoppingCartPaymentSuccessViewModel>(userId);
                foreach (var item in items)
                {
                    await this.productsService.IncreaseTimesBoughtAsync(item.ProductId);
                }

                await this.shoppingCartsService.DeleteAllAsync(userId);
            }

            return this.View();
        }

        [Authorize]
        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var userId = this.userManager.GetUserId(this.User);
            var items = this.shoppingCartsService.GetAllByUserId<ShoppingCartViewModel>(userId);
            var itemOptions = items.Select(x => new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(x.Product.Price * 100),
                    Currency = "eur",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = x.Product.Name,
                        Images = new List<string>() { x.Product.ImageUrl },
                    },
                },
                Quantity = x.Quantity,
            }).ToList();

            // Shipping fee
            itemOptions.Add(new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = 500,
                    Currency = "eur",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Shipping",
                    },
                },
                Quantity = 1,
            });

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card", },
                LineItems = itemOptions,
                Mode = "payment",
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "ES" },
                },
                SuccessUrl = "https://localhost:44319/payments/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:44319/shoppingcart",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return this.Json(new { id = session.Id });
        }
    }
}
