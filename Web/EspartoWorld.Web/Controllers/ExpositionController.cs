namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using Microsoft.AspNetCore.Mvc;

    public class ExpositionController : BaseController
    {
        private readonly IExposicionItemService expositionItemService;

        public ExpositionController(IExposicionItemService exposicionItemService)
        {
            this.expositionItemService = exposicionItemService;
        }

        public IActionResult Add()
        {
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
