namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExposicionItemService
    {
        Task<int> AddAsync<T>(T input);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int itemId);
    }
}
