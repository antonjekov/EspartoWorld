namespace EspartoWorld.Web.ViewModels.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class QuantityChangeInputModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Quantity should be number between {1} and {2}")]
        public int Quantity { get; set; }
    }
}
