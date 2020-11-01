using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EspartoWorld.Web.Controllers
{
    public class ExpositionController : BaseController
    {
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync()
        {
            //var id = await this.coursesService.AddAsync(input);
            return this.Redirect("/");
        }

        public IActionResult All()
        {
            //var courses = this.coursesService.GetAll<CourseViewModel>();
            return this.View();
        }
    }
}
