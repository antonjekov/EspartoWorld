namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVideosService
    {
        Task<int> AddAsync<T>(T input);

        ICollection<T> GetAll<T>();

        T GetById<T>(int videoId);

        T GetLastVideo<T>();

        Task DeleteAsync(int videoId);

        Task Edit<T>(T input);
    }
}
