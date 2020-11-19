namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using EspartoWorld.Web.ViewModels.ExpositionItems;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.userManager.GetUserId(this.User);
            this.ViewData["userId"] = userId;
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAsync(ExpositionItemInputModel input)
        {
            await this.expositionItemService.AddAsync(input);
            return this.Redirect("/Exposition/ThankYou");
        }

        public IActionResult All()
        {
            var items = this.expositionItemService.GetAllAccepted<ExpositionItemViewModel>();
            return this.View(items);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Details(int id)
        {
            if (!this.expositionItemService.IdIsValid(id))
            {
                return this.Redirect("/Exposition/Moderate");
            }

            var item = this.expositionItemService.GetById<ExpositionItemModerateModel>(id);
            return this.View(item);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Moderate()
        {
            var items = this.expositionItemService.GetAllForModerate<ExpositionItemViewModel>();
            return this.View(items);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return this.Redirect("/Exposition/All");
            }

            await this.expositionItemService.Delete(id);
            return this.Redirect("/Exposition/All");
        }
    }
}
