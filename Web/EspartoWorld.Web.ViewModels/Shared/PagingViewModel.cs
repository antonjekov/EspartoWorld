namespace EspartoWorld.Web.ViewModels.Shared
{
    using System;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public int ArtworksCount { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.ArtworksCount / this.ItemsPerPage);

        public int ItemsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public string AuthorID { get; set; }
    }
}
