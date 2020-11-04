namespace EspartoWorld.Web.ViewModels.ExposicionItems
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;

    public class ExpositionItemInputModel
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
    }
}
