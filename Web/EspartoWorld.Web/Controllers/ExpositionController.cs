namespace EspartoWorld.Web.Controllers
{
    using System.Linq;
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
        private readonly IVotesService votesService;
        private readonly IExposicionItemService expositionItemService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExpositionController(IVotesService votesService, IExposicionItemService exposicionItemService, UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
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
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);
            input.AuthorId = userId;
            await this.expositionItemService.AddAsync(input);
            return this.Redirect("/Exposition/ThankYou");
        }

        public IActionResult All(string author, Category itemCategory, int id = 1)
        {
            int itemsPerPage = 3;
            var items = this.expositionItemService.GetAllAccepted<ExpositionItemViewModel>(id, itemsPerPage,  itemCategory, author: author);
            foreach (var item in items)
            {
                item.AverageVotes = this.votesService.GetAverageVotes(item.Id);
                item.VotesCount = this.votesService.GetVotesCount(item.Id);
            }

            var artworksCount = this.expositionItemService.GetCountAccepted(author, itemCategory);
            var artworks = new ExpositionItemViewModelPagination()
            {
                PageNumber = id,
                ExpositionItems = items,
                ItemsCount = artworksCount,
                ItemsPerPage = itemsPerPage,
                AuthorID = author,
                ItemCategory = (int)itemCategory,
            };
            return this.View(artworks);
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        [Authorize]
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
