using EspartoWorld.Data.Models;
using EspartoWorld.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace EspartoWorld.Web.ViewModels.ExpositionItems
{
    public class ExpositionItemModerateViewModel : IMapFrom<ExpositionItem>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public double Price { get; set; }

        public bool IsSold { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public bool Accepted { get; set; }
    }
}
