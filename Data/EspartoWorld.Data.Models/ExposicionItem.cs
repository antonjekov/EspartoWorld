namespace EspartoWorld.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Common.Models;

    public class ExposicionItem : BaseDeletableModel<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public Category Category { get; set; }

        public double Price { get; set; }

        [Required]
        public bool IsSold { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}
