namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVideosService
    {
        Task<int> AddAsync<T>(T input);

        ICollection<T> GetAll<T>();

        T GetById<T>(int courseId);

        T GetLastVideo<T>();
    }
}
