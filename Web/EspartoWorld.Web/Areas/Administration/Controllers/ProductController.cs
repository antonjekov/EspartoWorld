namespace EspartoWorld.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.Controllers;
    using EspartoWorld.Web.ViewModels.Manufacturers;
    using EspartoWorld.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ProductController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IManufacturersService manufacturersService;

        public ProductController(IProductsService productsService, IManufacturersService manufacturersService)
        {
            this.productsService = productsService;
            this.manufacturersService = manufacturersService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductInputModel input)
        {
            // Case new Manufacturer with existing Id
            if (this.manufacturersService.IdExists(input.ManufacturerInput.Id) && input.ManufacturerId == null)
            {
                this.ModelState.AddModelError("ManufacturerInput.Id", "Manufacturer with this NIF already exists");
                return this.View();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // Case new Manufacturer
            if (input.ManufacturerId == null)
            {
                var manufacturerId = await this.manufacturersService.AddAsync<ManufacturerInputModel>(input.ManufacturerInput);
                input.ManufacturerId = manufacturerId;
            }

            var id = await this.productsService.AddAsync<ProductInputModel>(input);
            return this.RedirectToRoute(new { area = string.Empty, controller = "Product", action = "Details", id = id });
        }
    }
}
