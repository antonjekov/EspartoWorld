namespace EspartoWorld.Web.Controllers
{
    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CoursesController : BaseController
    {
        private readonly ICoursesService coursesService;
        private readonly IDeletableEntityRepository<Course> repository;

        public CoursesController(ICoursesService coursesService, IDeletableEntityRepository<Course> repository)
        {
            this.coursesService = coursesService;
            this.repository = repository;
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
