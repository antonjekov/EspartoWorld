namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVideosService
    {
        Task<int> AddAsync<T>(T input);

        ICollection<T> GetAll<T>();

        ICollection<T> GetAll<T>(int page, int itemsPerPage);

        int GetCountAllVideos();

        T GetById<T>(int videoId);

        T GetLastVideo<T>();

        Task DeleteAsync(int videoId);

        Task Edit<T>(T input);
    }
}
