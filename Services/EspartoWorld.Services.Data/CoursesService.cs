namespace EspartoWorld.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<int> AddAsync<T>(T input)
        {
            var course = AutoMapperConfig.MapperInstance.Map<Course>(input);
            await this.courseRepository.AddAsync(course);
            await this.courseRepository.SaveChangesAsync();
            return course.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.courseRepository.All().Where(x => x.StartDate >= DateTime.Now).OrderBy(x => x.StartDate).To<T>().ToList();
        }

        public T GetById<T>(int courseId)
        {
            return this.courseRepository.All().Where(x => x.Id == courseId).To<T>().FirstOrDefault();
        }

        public T GetNextCourse<T>()
        {
            return this.courseRepository.All().Where(x => x.StartDate >= DateTime.Now).OrderBy(x => x.StartDate).To<T>().FirstOrDefault();
        }
    }
}
