namespace EspartoWorld.Web.ViewModels.Videos
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class VideoInputModel : IMapTo<Video>
    {
        [Required]
        public string VideoId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
