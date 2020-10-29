namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        Task<int> AddAsync<T>(T input);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int courseId);

        T GetNextCourse<T>();
    }
}
