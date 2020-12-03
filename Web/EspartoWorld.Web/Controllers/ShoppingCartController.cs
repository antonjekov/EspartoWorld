namespace EspartoWorld.Web.Controllers
{
    using System.Threading.Tasks;

    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Data;
    using EspartoWorld.Web.ViewModels.ShoppingCart;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ShoppingCartController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShoppingCartsService shoppingCartsService;

        public ShoppingCartController(UserManager<ApplicationUser> userManager, IShoppingCartsService shoppingCartsService)
        {
            this.userManager = userManager;
            this.shoppingCartsService = shoppingCartsService;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            var shoppingCartItems = this.shoppingCartsService.GetAllByUserId<ShoppingCartViewModel>(userId);
            var shipping = 5;
            var input = new ShoppingCartViewModelExtended(shipping) { ShoppingCart = shoppingCartItems };
            return this.View(input);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.shoppingCartsService.DeleteAsync(userId, id);
            return this.Redirect("/ShoppingCart/Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProductQuantityAsync(int id, QuantityChangeInputModel quantityModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/ShoppingCart/Index");
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.shoppingCartsService.UpdateQuantityAsync(userId, id, quantityModel.Quantity);
            return this.Redirect("/ShoppingCart/Index");
        }
    }
}
