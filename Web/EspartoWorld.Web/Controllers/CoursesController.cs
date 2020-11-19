namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
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
            var id = await this.coursesService.AddAsync(input);
            return this.Redirect($"/Courses/Details/{id}");
        }

        public IActionResult All()
        {
            var courses = this.coursesService.GetAll<CourseViewModel>();
            return this.View(courses);
        }

        public IActionResult Details(int id)
        {
            if (!this.coursesService.IdIsValid(id))
            {
                return this.Redirect("/Courses/All");
            }

            var course = this.coursesService.GetById<CourseViewModel>(id);
            return this.View(course);
        }
    }
}
