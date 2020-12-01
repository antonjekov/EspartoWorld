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
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAsync(ExpositionItemInputModel input)
        {
            // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value; //In this maner we can take user from cookie not to go to database
            var userId = this.userManager.GetUserId(this.User);
            input.AuthorId = userId;
            await this.expositionItemService.AddAsync(input);
            return this.Redirect("/Exposition/ThankYou");
        }

        public IActionResult All(int id = 1)
        {
            int itemsPerPage = 6;
            var items = this.expositionItemService.GetAllAccepted<ExpositionItemViewModel>(id, itemsPerPage);
            var artworksCount = this.expositionItemService.GetCountAccepted();
            var artworks = new ExpositionItemViewModelPagination()
            {
                PageNumber = id,
                ExpositionItems = items,
                ItemsCount = artworksCount,
                ItemsPerPage = itemsPerPage,
            };
            return this.View(artworks);
        }

        [HttpGet]
        public IActionResult AllOfAuthor(string author, int id = 1)
        {
            if (author == null)
            {
                author = this.userManager.GetUserId(this.User);
            }

            int itemsPerPage = 6;
            var items = this.expositionItemService.GetAllAcceptedByAuthorId<ExpositionItemViewModel>(author, id, itemsPerPage);
            var artworksCount = this.expositionItemService.GetCountAccepted(author);
            var artworks = new ExpositionItemViewModelPagination()
            {
                PageNumber = id,
                ExpositionItems = items,
                ItemsCount = artworksCount,
                ItemsPerPage = itemsPerPage,
                AuthorID = author,
            };
            return this.View("All", artworks);
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

            await this.expositionItemService.EditAsync<ExpositionItemModerateModel>(input);
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id == 0)
            {
                return this.Redirect("/Exposition/Moderate");
            }

            await this.expositionItemService.DeleteAsync(id);
            return this.Redirect("/Exposition/Moderate");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOwnItemAsync(int itemId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var item = this.expositionItemService.GetById<ExpositionItemModerateModel>(itemId);
            if (item.AuthorId == userId)
            {
                await this.expositionItemService.DeleteAsync(itemId);
            }

            return this.Redirect("/Exposition/All");
        }
    }
}
