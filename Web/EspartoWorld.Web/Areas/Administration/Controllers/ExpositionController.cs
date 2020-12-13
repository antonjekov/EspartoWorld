namespace EspartoWorld.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.Controllers;
    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using EspartoWorld.Web.ViewModels.ExpositionItems;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ExpositionController : BaseController
    {
        private readonly IVotesService votesService;
        private readonly IExposicionItemService expositionItemService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExpositionController(IVotesService votesService, IExposicionItemService exposicionItemService, UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.expositionItemService = exposicionItemService;
            this.userManager = userManager;
        }

        public IActionResult Details(int id)
        {
            if (!this.expositionItemService.IdIsValid(id))
            {
                return this.RedirectToAction("Moderate", "Exposition");
            }

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

            await this.expositionItemService.EditAsync<ExpositionItemModerateModel>(input);
            return this.RedirectToAction("Moderate", "Exposition");
        }

        public IActionResult Moderate()
        {
            var items = this.expositionItemService.GetAllForModerate<ExpositionItemViewModel>();
            return this.View(items);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return this.RedirectToAction("Moderate", "Exposition");
            }

            await this.expositionItemService.DeleteAsync(id);
            return this.RedirectToAction("Moderate", "Exposition");
        }
    }
}
