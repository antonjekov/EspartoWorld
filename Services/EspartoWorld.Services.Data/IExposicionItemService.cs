namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExposicionItemService
    {
        Task<int> AddAsync<T>(T input);

        IEnumerable<T> GetAll<T>();

        T GetLastExpositionItem<T>();

        IEnumerable<T> GetAllForModerate<T>();

        public IEnumerable<T> GetAllAccepted<T>(int page, int itemsPerPage);

        public IEnumerable<T> GetAllAcceptedByAuthorId<T>(string authorID, int page, int itemsPerPage);

        T GetById<T>(int itemId);

        bool IdIsValid(int id);

        Task Edit<T>(T input);

        Task Delete(int itemId);

        int GetCountAccepted();

        int GetCountAccepted(string authorID);
    }
}
