namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public void Add<T>(T input)
        {
            var mapper = AutoMapperConfig.MapperInstance;
            var course = mapper.Map<Course>(input);
            this.courseRepository.AddAsync(course);
            this.courseRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.courseRepository.All().To<T>().ToList();
        }

        public T GetById<T>(int courseId)
        {
            return this.courseRepository.All().Where(x => x.Id == courseId).To<T>().FirstOrDefault();
        }

        public T GetNextCourse<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
