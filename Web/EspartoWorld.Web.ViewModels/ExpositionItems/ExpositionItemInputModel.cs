namespace EspartoWorld.Web.ViewModels.ExposicionItems
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ExpositionItemInputModel : IMapTo<ExpositionItem>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public Category Category { get; set; }

        public double Price { get; set; }

        public bool IsSold { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(5000)]
        public string Description { get; set; }

        public string AuthorId { get; set; }
    }
}
