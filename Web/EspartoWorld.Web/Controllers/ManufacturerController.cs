namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Mvc;

    public class ManufacturerController : BaseController
    {
        private readonly IManufacturersService manufacturersService;

        public ManufacturerController(IManufacturersService manufacturersService)
        {
            this.manufacturersService = manufacturersService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ManufacturerInputModel input)
        {
            if (this.manufacturersService.IdExists(input.Id))
            {
                this.ModelState.AddModelError("Id", "Manufacturer with this NIF already exists");
                return this.View();
            }

            await this.manufacturersService.AddAsync(input);
            return this.Redirect("/Manufacturer/Add");
        }
    }
}
