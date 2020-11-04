namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ExpositionController : BaseController
    {
        private readonly IExposicionItemService expositionItemService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExpositionController(IExposicionItemService exposicionItemService, UserManager<ApplicationUser> userManager)
        {
            this.expositionItemService = exposicionItemService;
            this.userManager = userManager;
        }

        public IActionResult Add()
        {
            var userId = this.userManager.GetUserId(this.User);
            this.ViewData["userId"] = userId;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ExpositionItemInputModel input)
        {
            var id = await this.expositionItemService.AddAsync(input);
            return this.Redirect($"/Exposition/Details/{id}");
        }

        public IActionResult All()
        {
            var items = this.expositionItemService.GetAll<ExpositionItemViewModel>();
            return this.View(items);
        }

        public IActionResult Details(int id)
        {
            var item = this.expositionItemService.GetById<ExpositionItemViewModel>(id);
            return this.View(item);
        }
    }
}
