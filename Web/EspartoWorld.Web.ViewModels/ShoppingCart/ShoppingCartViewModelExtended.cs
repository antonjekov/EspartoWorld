namespace EspartoWorld.Web.ViewModels.ShoppingCart
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartViewModelExtended
    {
        public ShoppingCartViewModelExtended(decimal shipping)
        {
            this.Shipping = shipping;
        }

        public IEnumerable<ShoppingCartViewModel> ShoppingCart { get; set; }

        public decimal Shipping { get; }

        public decimal SubtotalPrice => this.ShoppingCart.Sum(x => x.Product.Price * x.Quantity);

        public decimal TotalPrice => this.SubtotalPrice + this.Shipping;

        public QuantityChangeInputModel QuantityChangeInputModel { get; set; }
    }
}
