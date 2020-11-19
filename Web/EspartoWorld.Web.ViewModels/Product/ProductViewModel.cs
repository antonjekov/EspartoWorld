namespace EspartoWorld.Web.ViewModels.Product
{
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public int TimesBought { get; set; } = 0;

        public string ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
    }
}
