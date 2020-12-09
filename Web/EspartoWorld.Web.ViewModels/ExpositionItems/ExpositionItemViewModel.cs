namespace EspartoWorld.Web.ViewModels.ExposicionItems
{
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ExpositionItemViewModel : IMapFrom<ExpositionItem>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public bool IsSold { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public double AverageVotes { get; set; }

        public int VotesCount { get; set; }
    }
}
