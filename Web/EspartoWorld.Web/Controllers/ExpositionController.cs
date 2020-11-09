namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using EspartoWorld.Web.ViewModels.ExpositionItems;
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
            return this.Redirect("/Exposition/ThankYou");
        }

        public IActionResult All()
        {
            var items = this.expositionItemService.GetAllAccepted<ExpositionItemViewModel>();
            return this.View(items);
        }

        public IActionResult Details(int id)
        {
            var item = this.expositionItemService.GetById<ExpositionItemModerateModel>(id);
            return this.View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Details(ExpositionItemModerateModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.expositionItemService.Edit<ExpositionItemModerateModel>(input);
            return this.Redirect("/Exposition/Moderate");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        public IActionResult Moderate()
        {
            var items = this.expositionItemService.GetAllForModerate<ExpositionItemViewModel>();
            return this.View(items);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.expositionItemService.Delete(id);
            return this.Redirect("/Exposition/All");
        }
    }
}
