namespace EspartoWorld.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Services.YouTube;
    using EspartoWorld.Web.ViewModels.Videos;
    using Google.Apis.YouTube.v3.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    public class VideosController : BaseController
    {
        private readonly IVideosService videosService;
        private readonly IYouTubeDataService youTubeDataService;
        private readonly IDistributedCache distributedCacheService;

        public VideosController(IVideosService videosService, IYouTubeDataService youTubeDataService, IDistributedCache distributedCacheService)
        {
            this.videosService = videosService;
            this.youTubeDataService = youTubeDataService;
            this.distributedCacheService = distributedCacheService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> AddAsync()
        {
            var info = await this.distributedCacheService.GetStringAsync("YouTubeEspartoSearchCache");
            List<SearchResult> newVideos = new List<SearchResult>();
            if (info == null)
            {
                newVideos.AddRange(await this.youTubeDataService.GetLastVideosAsync("esparto", 100));
                await this.distributedCacheService.SetStringAsync(
                    "YouTubeEspartoSearchCache",
                    JsonConvert.SerializeObject(newVideos),
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.UtcNow.AddMinutes(30),
                    });
            }
            else
            {
                newVideos.AddRange(JsonConvert.DeserializeObject<List<SearchResult>>(info));
            }

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
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Add", "Videos");
            }

            await this.videosService.AddAsync(input);
            return this.RedirectToAction("All", "Videos");
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
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", "Videos");
            }

            await this.videosService.EditAsync(input);
            var videos = this.videosService.GetAll<VideoEditInputViewModel>();
            return this.View(videos);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.videosService.DeleteAsync(id);
            return this.RedirectToAction("Edit", "Videos");
        }
    }
}
