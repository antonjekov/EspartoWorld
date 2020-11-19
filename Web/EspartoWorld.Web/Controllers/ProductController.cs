namespace EspartoWorld.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Manufacturers;
    using EspartoWorld.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IManufacturersService manufacturersService;

        public ProductController(IProductsService productsService, IManufacturersService manufacturersService)
        {
            this.productsService = productsService;
            this.manufacturersService = manufacturersService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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

        public IActionResult All()
        {
            var products = this.productsService.GetAllVisibleOrderedCreatedOn<ProductViewModel>();
            return this.View(products);
        }

        [HttpPost]
        public IActionResult All(int productSort)
        {
            var products = this.productsService.GetAllVisibleOrderedCreatedOn<ProductViewModel>();
            return productSort switch
            {
                1 => this.View(products.OrderBy(x => x.Price)),
                2 => this.View(products.OrderByDescending(x => x.Price)),
                3 => this.View(products.OrderByDescending(x => x.TimesBought)),
                _ => this.View(products),
            };
        }

        public IActionResult Details(int id)
        {
            if (!this.productsService.IdIsValid(id))
            {
                return this.Redirect("/Product/All");
            }

            var product = this.productsService.GetById<ProductViewModel>(id);
            return this.View(product);
        }
    }
}
