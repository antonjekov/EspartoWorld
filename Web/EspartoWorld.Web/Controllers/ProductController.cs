namespace EspartoWorld.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Common;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShoppingCartsService shoppingCartsService;

        public ProductController(IProductsService productsService, UserManager<ApplicationUser> userManager, IShoppingCartsService shoppingCartsService)
        {
            this.productsService = productsService;
            this.userManager = userManager;
            this.shoppingCartsService = shoppingCartsService;
        }

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
                return this.RedirectToAction("All", "Product");
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
        public async Task<IActionResult> AddToCartAsync(int id, int quantity)
        {
            if (!this.productsService.IdIsValid(id))
            {
                return this.RedirectToAction("All", "Product");
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.shoppingCartsService.AddAsync(userId, id, quantity);
            return this.RedirectToAction("Index", "ShoppingCart");
        }
    }
}
