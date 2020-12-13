namespace EspartoWorld.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.Controllers;
    using EspartoWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CoursesController : BaseController
    {
        private ICoursesService coursesService;
        private IStringLocalizer<CoursesController> localizer;

        public CoursesController(ICoursesService coursesService, IStringLocalizer<CoursesController> localizer)
        {
            this.coursesService = coursesService;
            this.localizer = localizer;
        }

        public IActionResult Add()
        {
            return this.View();
        }

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
            return this.RedirectToRoute(new { area=string.Empty, controller = "Courses", action = "Details", id = id });
        }

        public IActionResult Edit(int id)
        {
            var course = this.coursesService.GetById<CourseViewModel>(id);
            var input = new CourseEditModel() { Actual = course, Changed = new CourseEditInputModel() };
            return this.View(input);
        }

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
            return this.RedirectToRoute(new { area = string.Empty, controller = "Courses", action = "Details", id = id });
        }

        public IActionResult EditAll()
        {
            var courses = this.coursesService.GetAll<CourseViewModel>();
            return this.View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.coursesService.DeleteAsync(id);
            return this.RedirectToAction("EditAll");
        }
    }
}
