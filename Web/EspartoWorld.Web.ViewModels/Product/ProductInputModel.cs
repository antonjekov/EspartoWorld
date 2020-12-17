namespace EspartoWorld.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;
    using EspartoWorld.Web.ViewModels.Manufacturers;

    public class ProductInputModel : IMapTo<Product>
    {
        [Required(ErrorMessage = "Name of product is required")]
        [MinLength(3, ErrorMessage = "Name should be minimum 3 characters long.")]
        [MaxLength(150, ErrorMessage = "Name should be maximum 150 characters long.")]
        [Display(Name = "Product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description of product is required")]
        [MinLength(3, ErrorMessage = "Description should be minimum 3 characters long.")]
        [MaxLength(500, ErrorMessage = "Description should be maximum 500 characters long.")]
        [Display(Name = "Product description")]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity should be number between {1} and {2}")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Range(0, 1000, ErrorMessage = "Price should be between {1} and {2} EUR")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please upload image for this course")]
        [Url(ErrorMessage = "Please fill valid URL")]
        [Display(Name = "URL of image")]
        public string ImageUrl { get; set; }

        public string ManufacturerId { get; set; }

        public ManufacturerInputModel ManufacturerInput { get; set; }
    }
}
