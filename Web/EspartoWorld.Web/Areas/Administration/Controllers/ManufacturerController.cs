namespace EspartoWorld.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.Controllers;
    using EspartoWorld.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ManufacturerController : BaseController
    {
        private readonly IManufacturersService manufacturersService;

        public ManufacturerController(IManufacturersService manufacturersService)
        {
            this.manufacturersService = manufacturersService;
        }

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
            return this.RedirectToAction("Add", "Manufacturer");
        }

        public IActionResult GetManufacturerInfo(string id)
        {
            var manufacturer = this.manufacturersService.GetById<ManufacturersViewModel>(id);
            return this.Json(manufacturer);
        }
    }
}
