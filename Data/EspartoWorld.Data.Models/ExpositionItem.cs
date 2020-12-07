namespace EspartoWorld.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Common.Models;

    public class ExpositionItem : BaseDeletableModel<int>
    {
        public ExpositionItem()
        {
            this.IsSold = true;
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public Category Category { get; set; }

        public decimal Price { get; set; }

        public bool IsSold { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool Accepted { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
