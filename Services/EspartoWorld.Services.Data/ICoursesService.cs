namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Models;

    public interface ICoursesService
    {
        Task<int> AddAsync<T>(T input);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAll<T>(string userId);

        bool IdIsValid(int id);

        T GetById<T>(int courseId);

        T GetNextCourse<T>();

        bool UserAlreadyParticipatedToCourse(int courseId, string userId);

        Task AddUserToCourseAsync(int courseId, ApplicationUser user);

        Task EditAsync<T>(T input);

        Task DeleteAsync(int id);
    }
}
