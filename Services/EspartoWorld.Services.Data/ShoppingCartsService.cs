namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly IRepository<ShoppingCartItem> shoppingCartItems;

        public ShoppingCartsService(IRepository<ShoppingCartItem> shoppingCartItems)
        {
            this.shoppingCartItems = shoppingCartItems;
        }

        public IEnumerable<T> GetAllByUserId<T>(string userId)
        {
            return this.shoppingCartItems.All().Where(x => x.ApplicationUserId == userId).To<T>().ToList();
        }

        public async Task AddAsync(string userId, int productId, int quantity)
        {
            var productExistForUser = this.shoppingCartItems.All().FirstOrDefault(x => x.ApplicationUserId == userId && x.ProductId == productId);
            if (productExistForUser != null)
            {
                productExistForUser.Quantity += quantity;
                this.shoppingCartItems.Update(productExistForUser);
                await this.shoppingCartItems.SaveChangesAsync();
            }
            else
            {
                await this.shoppingCartItems.AddAsync(new ShoppingCartItem()
                {
                    ApplicationUserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                });
                await this.shoppingCartItems.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(string userId, int productId)
        {
            var item = this.shoppingCartItems.All().FirstOrDefault(x => x.ApplicationUserId == userId && x.ProductId == productId);
            if (item != null)
            {
                this.shoppingCartItems.Delete(item);
                await this.shoppingCartItems.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityAsync(string userId, int productId, int quantity)
        {
            if (quantity == 0)
            {
                await this.DeleteAsync(userId, productId);
                return;
            }

            var item = this.shoppingCartItems.All().FirstOrDefault(x => x.ApplicationUserId == userId && x.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                this.shoppingCartItems.Update(item);
                await this.shoppingCartItems.SaveChangesAsync();
            }
        }
    }
}
