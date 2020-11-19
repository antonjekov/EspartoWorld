namespace EspartoWorld.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Manufacturers;
    using EspartoWorld.Web.ViewModels.Product;
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

        public IActionResult All()
        {
            var products = this.productsService.GetAllVisibleOrderedCreatedOn<ProductViewModel>();
            return this.View(products);
        }

        [HttpPost]
        public IActionResult All(int productSort)
        {
            var products = this.productsService.GetAllVisibleOrderedCreatedOn<ProductViewModel>();
            switch (productSort)
            {
                case 1: return this.View(products.OrderBy(x => x.Price));
                case 2: return this.View(products.OrderByDescending(x => x.Price));
                case 3: return this.View(products.OrderByDescending(x => x.TimesBought));
                default: return this.View(products);
            }
        }

        public IActionResult Details(int id)
        {
            var product = this.productsService.GetById<ProductViewModel>(id);
            return this.View(product);
        }
    }
}
