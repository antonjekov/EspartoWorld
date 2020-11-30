namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        Task<int> AddAsync<T>(T input);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(string userId);

        bool IdIsValid(int id);

        T GetById<T>(int courseId);

        T GetNextCourse<T>();

        bool UserAlreadyParticipatedToCourse(int courseId, string userId);

        Task AddUserToCourseAsync(int courseId, string userId);

        Task EditAsync<T>(T input);
    }
}
