namespace EspartoWorld.Web.Controllers
{
    using System.Diagnostics;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels;
    using EspartoWorld.Web.ViewModels.Courses;
    using EspartoWorld.Web.ViewModels.Home;
    using EspartoWorld.Web.ViewModels.Videos;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IVideosService videosService;
        private readonly ICoursesService coursesService;

        public HomeController(IVideosService videosService, ICoursesService coursesService)
        {
            this.videosService = videosService;
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var modelInfo = new HomeViewModel()
            {
                LastVideo = this.videosService.GetLastVideo<VideoViewModel>(),
                NextCourse = this.coursesService.GetNextCourse<CourseViewModel>(),
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
