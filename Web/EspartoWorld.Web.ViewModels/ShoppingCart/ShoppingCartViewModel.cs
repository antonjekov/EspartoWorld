namespace EspartoWorld.Web.ViewModels.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ShoppingCartViewModel : IMapFrom<ShoppingCartItem>
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
