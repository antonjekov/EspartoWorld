namespace EspartoWorld.Web.Controllers
{
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Manufacturers;
    using EspartoWorld.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
        public async Task<IActionResult> Add(ProductInputModel input)
        {
            if (this.manufacturersService.IdExists(input.ManufacturerInput.Id))
            {
                this.ModelState.AddModelError("ManufacturerInput.Id", "Manufacturer with this NIF already exists");
                return this.View();
            }

            if (input.ManufacturerId == null)
            {
                var manufacturerId = await this.manufacturersService.AddAsync<ManufacturerInputModel>(input.ManufacturerInput);
                input.ManufacturerId = manufacturerId;
            }

            await this.productsService.AddAsync<ProductInputModel>(input);
            return this.Redirect("/");
        }
    }
}
