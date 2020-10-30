namespace EspartoWorld.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Services.YouTube;
    using EspartoWorld.Web.ViewModels.Videos;
    using Google.Apis.YouTube.v3.Data;
    using Microsoft.AspNetCore.Mvc;

    public class VideosController : BaseController
    {
        private readonly IVideosService videosService;
        private readonly IYouTubeDataService youTubeDataService;

        public VideosController(IVideosService videosService, IYouTubeDataService youTubeDataService)
        {
            this.videosService = videosService;
            this.youTubeDataService = youTubeDataService;
        }

        public async Task<IActionResult> Add()
        {
            var newVideos = await this.youTubeDataService.GetLastVideosAsync("esparto", 10);
            var newVideosViewModel = newVideos.Select(x => new VideoViewModel()
            {
                VideoId = x.Id.VideoId,
                Title = x.Snippet.Title,
            }).ToList();
            var ourVideosIdVideo = this.videosService.GetAll<VideoViewModel>();
            var videos = newVideosViewModel.Except(ourVideosIdVideo);

            return this.View(videos);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(VideoInputModel input)
        {
            await this.videosService.AddAsync(input);
            return this.Redirect("/Videos/All");
        }

        public IActionResult All()
        {
            var videos = this.videosService.GetAll<VideoViewModel>();
            return this.View(videos);
        }

        public IActionResult Play(string id)
        {
            this.ViewData["VideoId"] = id;
            return this.View("Play", id);
        }
    }
}
