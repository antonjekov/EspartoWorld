namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ManufacturerController : BaseController
    {
        private readonly IManufacturersService manufacturersService;

        public ManufacturerController(IManufacturersService manufacturersService)
        {
            this.manufacturersService = manufacturersService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddAsync(ManufacturerInputModel input)
        {
            if (this.manufacturersService.IdExists(input.Id))
            {
                this.ModelState.AddModelError("Id", "Manufacturer with this NIF already exists");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.manufacturersService.AddAsync(input);
            return this.Redirect("/Manufacturer/Add");
        }
    }
}
