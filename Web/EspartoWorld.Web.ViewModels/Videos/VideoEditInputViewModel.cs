namespace EspartoWorld.Web.ViewModels.Videos
{
    using System.ComponentModel.DataAnnotations;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class VideoEditInputViewModel : IMapTo<Video>, IMapFrom<Video>
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "VideoId is required")]
        public string VideoId { get; set; }

        [Required(ErrorMessage = "Please fill title of video")]
        public string Title { get; set; }
    }
}

