namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

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
            var course = this.coursesService.GetById<CourseViewModel>(id);
            return this.View(course);
        }
    }
}
