namespace EspartoWorld.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;
        private readonly IStringLocalizer<CoursesController> localizer;

        public CoursesController(ICoursesService coursesService, IStringLocalizer<CoursesController> localizer)
        {
            this.coursesService = coursesService;
            this.localizer = localizer;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddAsync(CourseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var id = await this.coursesService.AddAsync(input);
            var message = this.localizer["Course was successfully added"];
            this.TempData["Message"] = message.Value;
            return this.RedirectToAction("Details", "Courses", new { id });
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
                return this.Redirect("/Courses/All");
            }

            var course = this.coursesService.GetById<CourseViewModel>(id);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            course.SubscribedForCourse = this.coursesService.UserAlreadyParticipatedToCourse(course.Id, userId);
            return this.View(course);
        }

        [Authorize]
        public async Task<IActionResult> AddUserToCourseAsync(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (this.coursesService.UserAlreadyParticipatedToCourse(id, userId))
            {
                return this.Redirect($"/Courses/Details/{id}");
            }

            await this.coursesService.AddUserToCourseAsync(id, userId);
            return this.Redirect("/Courses/SubscribedForCourse");
        }

        [Authorize]
        public IActionResult SubscribedForCourse()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var course = this.coursesService.GetById<CourseViewModel>(id);
            var input = new CourseEditModel() { Actual = course, Changed = new CourseEditInputModel() };
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> EditAsync(CourseEditInputModel changed, int id)
        {
            if (!this.ModelState.IsValid)
            {
                var course = this.coursesService.GetById<CourseViewModel>(id);
                var input = new CourseEditModel() { Actual = course, Changed = new CourseEditInputModel() };
                return this.View(input);
            }

            changed.Id = id;
            await this.coursesService.EditAsync<CourseEditInputModel>(changed);
            return this.Redirect($"/Courses/Details/{id}");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult EditAll()
        {
            var courses = this.coursesService.GetAll<CourseViewModel>();
            return this.View(courses);
        }
    }
}
