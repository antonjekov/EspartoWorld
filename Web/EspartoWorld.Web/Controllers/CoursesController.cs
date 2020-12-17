namespace EspartoWorld.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;
        private readonly IStringLocalizer<CoursesController> localizer;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(ICoursesService coursesService, IStringLocalizer<CoursesController> localizer, UserManager<ApplicationUser> userManager)
        {
            this.coursesService = coursesService;
            this.localizer = localizer;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var courses = this.coursesService.GetAll<CourseViewModel>();
            return this.View(courses);
        }

        [Authorize]
        public IActionResult MyCourses()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var courses = this.coursesService.GetAll<CourseViewModel>(userId);
            return this.View("All", courses);
        }

        public IActionResult Details(int id)
        {
            if (!this.coursesService.IdIsValid(id))
            {
                return this.RedirectToAction("All", "Courses");
            }

            var course = this.coursesService.GetById<CourseViewModel>(id);
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                course.SubscribedForCourse = this.coursesService.UserAlreadyParticipatedToCourse(course.Id, userId);
            }

            return this.View(course);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUserToCourseAsync(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (this.coursesService.UserAlreadyParticipatedToCourse(id, userId))
            {
                return this.RedirectToAction("Details", "Courses", new { id });
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.coursesService.AddUserToCourseAsync(id, user);
            return this.RedirectToAction("SubscribedForCourse", "Courses");
        }

        [Authorize]
        public IActionResult SubscribedForCourse()
        {
            return this.View();
        }
    }
}
