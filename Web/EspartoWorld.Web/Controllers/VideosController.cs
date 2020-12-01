﻿namespace EspartoWorld.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Services.YouTube;
    using EspartoWorld.Web.ViewModels.Videos;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> AddAsync()
        {
            var newVideos = await this.youTubeDataService.GetLastVideosAsync("esparto", 100);
            var ourVideosVideoId = this.videosService.GetAll<VideoViewModel>().Select(x => x.VideoId).ToList();
            var videos = newVideos.Where(x => !ourVideosVideoId.Contains(x.Id.VideoId)).Select(x => new VideoViewModel()
            {
                VideoId = x.Id.VideoId,
                Title = x.Snippet.Title,
            }).ToList();
            return this.View(videos);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddAsync(VideoInputModel input)
        {
            await this.videosService.AddAsync(input);
            return this.Redirect("/Videos/All");
        }

        public IActionResult All(int id = 1)
        {
            int itemsPerPage = 8;
            var items = this.videosService.GetAll<VideoViewModel>(id, itemsPerPage);
            var itemsCount = this.videosService.GetCountAllVideos();
            var videos = new VideoViewModelPagination()
            {
                PageNumber = id,
                Videos = items,
                ItemsCount = itemsCount,
                ItemsPerPage = itemsPerPage,
            };
            return this.View(videos);
        }

        public IActionResult Play(string id)
        {
            this.ViewData["VideoId"] = id;
            return this.View("Play", id);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit()
        {
            var videos = this.videosService.GetAll<VideoEditInputViewModel>();
            return this.View(videos);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> EditAsync(VideoEditInputViewModel input)
        {
            await this.videosService.EditAsync(input);
            var videos = this.videosService.GetAll<VideoEditInputViewModel>();
            return this.View(videos);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.videosService.DeleteAsync(id);
            return this.Redirect("/Videos/Edit");
        }
    }
}
