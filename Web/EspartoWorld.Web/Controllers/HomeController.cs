namespace EspartoWorld.Web.Controllers
{
    using System.Diagnostics;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels;
    using EspartoWorld.Web.ViewModels.Courses;
    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using EspartoWorld.Web.ViewModels.Home;
    using EspartoWorld.Web.ViewModels.Videos;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IVideosService videosService;
        private readonly ICoursesService coursesService;
        private readonly IExposicionItemService exposicionItemService;

        public HomeController(IVideosService videosService, ICoursesService coursesService, IExposicionItemService exposicionItemService)
        {
            this.videosService = videosService;
            this.coursesService = coursesService;
            this.exposicionItemService = exposicionItemService;
        }

        public IActionResult Index()
        {
            var modelInfo = new HomeViewModel()
            {
                LastVideo = this.videosService.GetLastVideo<VideoViewModel>(),
                NextCourse = this.coursesService.GetNextCourse<CourseViewModel>(),
                LastArtwork = this.exposicionItemService.GetLastExpositionItem<ExpositionItemViewModel>(),
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
