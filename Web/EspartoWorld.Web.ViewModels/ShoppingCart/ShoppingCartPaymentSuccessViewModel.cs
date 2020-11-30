namespace EspartoWorld.Web.ViewModels.ShoppingCart
{
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ShoppingCartPaymentSuccessViewModel : IMapFrom<ShoppingCartItem>
    {
        public int ProductId { get; set; }
    }
}
