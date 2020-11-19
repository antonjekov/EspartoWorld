namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<int> AddAsync<T>(T input);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllVisibleOrderedCreatedOn<T>();
    }
}
