namespace EspartoWorld.Web.ViewModels.ExposicionItems
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;
    using EspartoWorld.Web.Infrastructure;

    public class ExpositionItemInputModel : IMapTo<ExpositionItem>
    {
        [Required(ErrorMessage ="Title for this artwork is required")]
        [MinLength(3, ErrorMessage = "Title should be minimum 3 characters long.")]
        [MaxLength(150, ErrorMessage = "Title should be maximum 150 characters long.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Please choose category.")]
        [Display(Name = "Category")]
        public Category Category { get; set; }

        [Display(Name = "Price")]
        [Range(0, 1000, ErrorMessage = "Price should be between {1} and {2} EUR")]
        public decimal Price { get; set; }

        [Display(Name = "Is sold")]
        public bool IsSold { get; set; }

        [Required(ErrorMessage ="Image url is required")]
        [Url(ErrorMessage ="This is not a valid URL")]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage ="Description for this artwork is required")]
        [MinLength(10, ErrorMessage = "Description should be minimum 10 characters long.")]
        [MaxLength(5000, ErrorMessage = "Title should be maximum 5000 characters long.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string AuthorId { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}
