namespace EspartoWorld.Web.ViewModels.Manufacturers
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ManufacturerInputModel : IMapTo<Manufacturer>
    {
        [Required(ErrorMessage = "Name of manufacturer is required")]
        [MinLength(3, ErrorMessage = "Name should be minimum 3 characters long.")]
        [MaxLength(150, ErrorMessage = "Name should be maximum 150 characters long.")]
        [Display(Name = "Manufacturer name")]
        public string Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "NIF should be {1} characters long.", MinimumLength = 10)]
        [Display(Name = "NIF")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        [Phone]
        [Display(Name = "Telephone number")]
        public int Telephone { get; set; }

        [Required(ErrorMessage = "Address of manufacturer is required")]
        [MinLength(20, ErrorMessage = "Address should be minimum 20 characters long.")]
        [Display(Name = "Address of manufacturer")]
        public string Address { get; set; }
    }
}
