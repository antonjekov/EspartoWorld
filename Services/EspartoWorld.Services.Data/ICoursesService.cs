namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;

    using EspartoWorld.Data.Models;

    public interface ICoursesService
    {
        void Add<T>(T input);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(int courseId);

        T GetNextCourse<T>();
    }
}
