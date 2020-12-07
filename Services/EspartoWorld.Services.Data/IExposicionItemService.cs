namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Models;

    public interface IExposicionItemService
    {
        Task<int> AddAsync<T>(T input);

        IEnumerable<T> GetAll<T>();

        T GetLastExpositionItem<T>();

        IEnumerable<T> GetAllForModerate<T>();

        public IEnumerable<T> GetAllAccepted<T>(int page, int itemsPerPage, Category itemCategory = 0, string author = null);

        T GetById<T>(int itemId);

        bool IdIsValid(int id);

        Task EditAsync<T>(T input);

        Task DeleteAsync(int itemId);

        int GetCountAccepted(string authorID = null, Category itemCategory = 0);
    }
}
