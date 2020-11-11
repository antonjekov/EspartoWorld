namespace EspartoWorld.Web.ViewModels.ExpositionItems
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ExpositionItemModerateModel : IMapTo<ExpositionItem>, IMapFrom<ExpositionItem>
    {
        public string Id { get; set; }

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

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public bool Accepted { get; set; }
    }
}
