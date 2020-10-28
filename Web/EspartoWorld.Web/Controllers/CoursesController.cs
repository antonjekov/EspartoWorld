namespace EspartoWorld.Web.Controllers
{
    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Course;
    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Add(CourseInputModel input)
        {
            this.coursesService.Add(input);
            return this.Redirect("/Courses/All");
        }

        public IActionResult All()
        {
            var courses = this.coursesService.GetAll<CourseViewModel>();
            return this.View(courses);
        }

        public IActionResult Details(int courseId)
        {
            var course = this.coursesService.GetById<CourseViewModel>(courseId);
            return this.View(course);
        }
    }
}
