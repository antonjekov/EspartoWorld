namespace EspartoWorld.Web.ViewModels.Videos
{
    using System.Collections.Generic;

    using EspartoWorld.Web.ViewModels.Shared;

    public class VideoViewModelPagination : PagingViewModel
    {
        public IEnumerable<VideoViewModel> Videos { get; set; }
    }
}
