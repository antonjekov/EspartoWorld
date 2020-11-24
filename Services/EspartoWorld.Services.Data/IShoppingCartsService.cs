namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShoppingCartsService
    {
        Task AddAsync(string userId, int productId, int quantity);

        IEnumerable<T> GetAllByUserId<T>(string userId);

        Task DeleteAsync(string userId, int productId);

        Task UpdateQuantityAsync(string userId, int productId, int quantity);
    }
}
