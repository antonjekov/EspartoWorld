namespace EspartoWorld.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;

    public class ProductDetailsModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Quantity should be number between {1} and {2}")]
        public int Quantity { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
