namespace EspartoWorld.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Services.YouTube;
    using EspartoWorld.Web.Controllers;
    using EspartoWorld.Web.ViewModels.Videos;
    using Google.Apis.YouTube.v3.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class VideosController : BaseController
    {
        private readonly IVideosService videosService;
        private readonly IYouTubeDataService youTubeDataService;
        private readonly IDistributedCache distributedCacheService;
        private readonly IMemoryCache memoryCache;

        public VideosController(IVideosService videosService, IYouTubeDataService youTubeDataService, IDistributedCache distributedCacheService, IMemoryCache memoryCache)
        {
            this.videosService = videosService;
            this.youTubeDataService = youTubeDataService;
            this.distributedCacheService = distributedCacheService;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> AddAsync()
        {
            // This variant is if we want to wark with distributed cache
            //var info = await this.distributedCacheService.GetStringAsync("YouTubeEspartoSearchCache");
            //List<SearchResult> newVideos = new List<SearchResult>();
            //if (info == null)
            //{
            //    newVideos.AddRange(await this.youTubeDataService.GetLastVideosAsync("esparto", 100));
            //    await this.distributedCacheService.SetStringAsync(
            //        "YouTubeEspartoSearchCache",
            //        JsonConvert.SerializeObject(newVideos),
            //        new DistributedCacheEntryOptions()
            //        {
            //            AbsoluteExpiration = DateTime.UtcNow.AddMinutes(30),
            //        });
            //}
            //else
            //{
            //    newVideos.AddRange(JsonConvert.DeserializeObject<List<SearchResult>>(info));
            //}

            List<SearchResult> newVideos;
            if (!this.memoryCache.TryGetValue("YouTubeEspartoSearchCache", out newVideos))
            {
                newVideos = await this.youTubeDataService.GetLastVideosAsync("esparto", 100);
                this.memoryCache.Set("YouTubeEspartoSearchCache", newVideos, TimeSpan.FromMinutes(30));
            }

            var ourVideosVideoId = this.videosService.GetAll<VideoViewModel>().Select(x => x.VideoId).ToList();
            var videos = newVideos.Where(x => !ourVideosVideoId.Contains(x.Id.VideoId)).Select(x => new VideoViewModel()
            {
                VideoId = x.Id.VideoId,
                Title = x.Snippet.Title,
            }).ToList();
            return this.View(videos);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(VideoInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Add", "Videos");
            }

            await this.videosService.AddAsync(input);
            return this.RedirectToRoute(new { area = string.Empty, controller = "Videos", action = "All" });
        }

        public IActionResult Edit()
        {
            var videos = this.videosService.GetAll<VideoEditInputViewModel>();
            return this.View(videos);
        }

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

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.videosService.DeleteAsync(id);
            return this.RedirectToAction("Edit", "Videos");
        }
    }
}
