namespace EspartoWorld.Web.ViewModels.Videos
{
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class VideoViewModel : IMapFrom<Video>
    {
        public string VideoId { get; set; }

        public string Title { get; set; }
    }
}
