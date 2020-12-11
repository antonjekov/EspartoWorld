namespace EspartoWorld.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

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

        public IEnumerable<T> GetAll<T>(string userId)
        {
            return this.courseRepository.All()
                .Where(x => x.StartDate >= DateTime.Now && x.Participants.Any(x => x.Id == userId))
                .OrderBy(x => x.StartDate)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int courseId)
        {
            return this.courseRepository.All().Where(x => x.Id == courseId).To<T>().FirstOrDefault();
        }

        public T GetNextCourse<T>()
        {
            return this.courseRepository.All().Where(x => x.StartDate >= DateTime.Now).OrderBy(x => x.StartDate).To<T>().FirstOrDefault();
        }

        public bool IdIsValid(int id)
        {
            return this.courseRepository.AllAsNoTracking().Any(x => x.Id == id);
        }

        public bool UserAlreadyParticipatedToCourse(int courseId, string userId)
        {
            return this.courseRepository.All().Any(x => x.Id == courseId && x.Participants.Any(p => p.Id == userId));
        }

        public async Task AddUserToCourseAsync(int courseId, ApplicationUser user)
        {
            var course = this.courseRepository.All().FirstOrDefault(x => x.Id == courseId);
            if (course != null)
            {
                course.Participants.Add(user);
                await this.courseRepository.SaveChangesAsync();
            }
        }

        public async Task EditAsync<T>(T input)
        {
            var item = AutoMapperConfig.MapperInstance.Map<Course>(input);
            this.courseRepository.Update(item);
            await this.courseRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = this.courseRepository.All().FirstOrDefault(x => x.Id == id);
            this.courseRepository.Delete(item);
            await this.courseRepository.SaveChangesAsync();
        }
    }
}
