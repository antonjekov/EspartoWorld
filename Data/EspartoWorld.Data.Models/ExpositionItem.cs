namespace EspartoWorld.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Common.Models;

    public class ExpositionItem : BaseDeletableModel<int>
    {
        public ExpositionItem()
        {
            this.IsSold = true;
        }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public Category Category { get; set; }

        public double Price { get; set; }

        public bool IsSold { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public bool Accepted { get; set; }
    }
}
