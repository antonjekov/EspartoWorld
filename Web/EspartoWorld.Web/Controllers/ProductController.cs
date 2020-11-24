namespace EspartoWorld.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Manufacturers;
    using EspartoWorld.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IManufacturersService manufacturersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShoppingCartsService shoppingCartsService;

        public ProductController(IProductsService productsService, IManufacturersService manufacturersService, UserManager<ApplicationUser> userManager, IShoppingCartsService shoppingCartsService)
        {
            this.productsService = productsService;
            this.manufacturersService = manufacturersService;
            this.userManager = userManager;
            this.shoppingCartsService = shoppingCartsService;
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
            var input = new ProductDetailsModel()
            {
                Product = product,
                Quantity = 1,
            };
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.ClientRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int quantity)
        {
            if (!this.productsService.IdIsValid(id))
            {
                return this.Redirect("/Product/All");
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.shoppingCartsService.AddAsync(userId, id, quantity);
            return this.Redirect("/ShoppingCart/Index");
        }
    }
}
