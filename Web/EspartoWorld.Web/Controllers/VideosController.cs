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

        public VideosController(IVideosService videosService)
        {
            this.videosService = videosService;
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
    }
}
