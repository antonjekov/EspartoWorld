namespace EspartoWorld.Web.Controllers
{
    using System;
    using System.Diagnostics;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels;
    using EspartoWorld.Web.ViewModels.Courses;
    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using EspartoWorld.Web.ViewModels.Home;
    using EspartoWorld.Web.ViewModels.Videos;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class HomeController : BaseController
    {
        private readonly IVideosService videosService;
        private readonly ICoursesService coursesService;
        private readonly IExposicionItemService exposicionItemService;
        private readonly IMemoryCache memoryCache;

        public HomeController(IVideosService videosService, ICoursesService coursesService, IExposicionItemService exposicionItemService, IMemoryCache memoryCache)
        {
            this.videosService = videosService;
            this.coursesService = coursesService;
            this.exposicionItemService = exposicionItemService;
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            if (!this.memoryCache.TryGetValue("LastVideo", out VideoViewModel lastVideo))
            {
                lastVideo = this.videosService.GetLastVideo<VideoViewModel>();
                this.memoryCache.Set("LastVideo", lastVideo, TimeSpan.FromMinutes(30));
            }

            if (!this.memoryCache.TryGetValue("NextCourse", out CourseViewModel nextCourse))
            {
                nextCourse = this.coursesService.GetNextCourse<CourseViewModel>();
                this.memoryCache.Set("NextCourse", nextCourse, TimeSpan.FromHours(6));
            }

            if (!this.memoryCache.TryGetValue("LastArtwork", out ExpositionItemViewModel lastArtwork))
            {
                lastArtwork = this.exposicionItemService.GetLastExpositionItem<ExpositionItemViewModel>();
                this.memoryCache.Set("LastArtwork", nextCourse, TimeSpan.FromMinutes(5));
            }

            var modelInfo = new HomeViewModel()
            {
                LastVideo = lastVideo,
                NextCourse = nextCourse,
                LastArtwork = lastArtwork,
            };
            return this.View(modelInfo);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
