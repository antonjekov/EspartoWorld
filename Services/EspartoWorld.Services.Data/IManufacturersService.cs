namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IManufacturersService
    {
        Task<string> AddAsync<T>(T input);

        bool IdExists(string id);

        IEnumerable<T> GetAll<T>();
    }
}
