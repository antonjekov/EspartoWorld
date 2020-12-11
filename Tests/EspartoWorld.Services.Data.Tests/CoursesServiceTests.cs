namespace EspartoWorld.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using EspartoWorld.Data;
    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Data.Repositories;
    using EspartoWorld.Services.Mapping;
    using EspartoWorld.Web.ViewModels;
    using EspartoWorld.Web.ViewModels.Courses;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CoursesServiceTests
    {
        [Fact]
        public void GetAllReturnsCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var list = new List<Course>()
            {
                new Course() { StartDate = DateTime.Now.AddDays(1), Title = "Last" },
                new Course() { StartDate = DateTime.Now, Title = "First" },
                new Course() { StartDate = DateTime.Now.AddHours(1), Title = "Second" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new CoursesService(mockRepo.Object);
            var actual = service.GetAll<CourseViewModel>().ToList();
            var expected = new List<CourseViewModel>()
            {
                new CourseViewModel() { Title = "Second" },
                new CourseViewModel() { Title = "Last" },
            };
            Assert.Equal(expected[0].Title, actual[0].Title);
            Assert.Equal(expected[1].Title, actual[1].Title);
        }

        [Fact]
        public void GetAllWithUserIdReturnsCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var list = new List<Course>()
            {
                new Course() { StartDate = DateTime.Now.AddDays(1), Title = "Last" },
                new Course() { StartDate = DateTime.Now, Title = "First" },
                new Course() { StartDate = DateTime.Now.AddHours(1), Title = "Second", Participants = new List<ApplicationUser>() { new ApplicationUser() { Id = "anyid" } } },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new CoursesService(mockRepo.Object);
            var actual = service.GetAll<CourseViewModel>("anyid").ToList();
            var expected = new List<CourseViewModel>()
            {
                new CourseViewModel() { Title = "Second" },
            };
            Assert.Equal(expected[0].Title, actual[0].Title);
        }

        [Fact]
        public void GetByIdReturnsCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var list = new List<Course>()
            {
                new Course() { Id = 5, StartDate = DateTime.Now.AddDays(1), Title = "Last" },
                new Course() { Id = 1, StartDate = DateTime.Now, Title = "First" },
                new Course() { Id = 8, StartDate = DateTime.Now.AddHours(1), Title = "Second", Participants = new List<ApplicationUser>() { new ApplicationUser() { Id = "anyid" } } },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new CoursesService(mockRepo.Object);
            var actual = service.GetById<CourseViewModel>(1);
            Assert.Equal("First", actual.Title);
        }

        [Fact]
        public void GetNextCourseReturnsCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var list = new List<Course>()
            {
                new Course() { Id = 5, StartDate = DateTime.Now.AddDays(1), Title = "Last" },
                new Course() { Id = 1, StartDate = DateTime.Now, Title = "First" },
                new Course() { Id = 8, StartDate = DateTime.Now.AddHours(1), Title = "Second", Participants = new List<ApplicationUser>() { new ApplicationUser() { Id = "anyid" } } },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new CoursesService(mockRepo.Object);
            var actual = service.GetNextCourse<CourseViewModel>();
            Assert.Equal("Second", actual.Title);
        }

        [Fact]
        public void GetNextCourseReturnsNull()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var list = new List<Course>()
            {
                new Course() { Id = 1, StartDate = DateTime.Now, Title = "First" },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new CoursesService(mockRepo.Object);
            var actual = service.GetNextCourse<CourseViewModel>();
            Assert.Null(actual);
        }

        [Fact]
        public void IdIsValidReturnsCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var list = new List<Course>()
            {
                new Course() { Id = 5, StartDate = DateTime.Now.AddDays(1), Title = "Last" },
                new Course() { Id = 1, StartDate = DateTime.Now, Title = "First" },
                new Course() { Id = 8, StartDate = DateTime.Now.AddHours(1), Title = "Second", Participants = new List<ApplicationUser>() { new ApplicationUser() { Id = "anyid" } } },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new CoursesService(mockRepo.Object);
            Assert.True(service.IdIsValid(5));
            Assert.False(service.IdIsValid(2));
        }

        [Fact]
        public void UserAlreadyParticipatedToCourseReturnsCorrect()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var list = new List<Course>()
            {
                new Course() { Id = 5, StartDate = DateTime.Now.AddDays(1), Title = "Last" },
                new Course() { Id = 1, StartDate = DateTime.Now, Title = "First" },
                new Course() { Id = 8, StartDate = DateTime.Now.AddHours(1), Title = "Second", Participants = new List<ApplicationUser>() { new ApplicationUser() { Id = "anyid" } } },
            };

            var mockRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new CoursesService(mockRepo.Object);
            Assert.True(service.UserAlreadyParticipatedToCourse(8, "anyid"));
            Assert.False(service.UserAlreadyParticipatedToCourse(2, "anyid"));
        }

        [Fact]
        public async Task AddsCorrectAddAsync()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CourseTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Courses.Add(new Course() { Id = 2, Title = "Existing" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Course>(dbContext);
            var service = new CoursesService(repository);
            var actual = await service.AddAsync(new CourseInputModel() { Title = "Second" });
            Assert.Equal(2, dbContext.Courses.Count());
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task DeletesCorrectDeleteAsync()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CourseTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Courses.Add(new Course() { Id = 1, Title = "First" });
            dbContext.Courses.Add(new Course() { Id = 2, Title = "Second" });
            dbContext.Courses.Add(new Course() { Id = 3, Title = "Third" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Course>(dbContext);
            var service = new CoursesService(repository);
            Assert.Equal(3, dbContext.Courses.Count());
            await service.DeleteAsync(3);
            Assert.Equal(2, dbContext.Courses.Count());
            await Assert.ThrowsAsync<NullReferenceException>(() => service.DeleteAsync(5));
            Assert.Equal(2, dbContext.Courses.Count());
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddsCorrectAddUserToCourseAsync()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CourseTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Courses.Add(new Course() { Id = 1, Title = "First" });
            dbContext.Courses.Add(new Course() { Id = 2, Title = "Second" });
            dbContext.Courses.Add(new Course() { Id = 3, Title = "Third" });
            await dbContext.SaveChangesAsync();

            var user = new ApplicationUser() { Id = "anyid" };

            using var repository = new EfDeletableEntityRepository<Course>(dbContext);
            var service = new CoursesService(repository);
            await service.AddUserToCourseAsync(2, user);
            Assert.Single(dbContext.Courses.First(x => x.Id == 2).Participants);
            await service.AddUserToCourseAsync(2, user);
            Assert.Single(dbContext.Courses.First(x => x.Id == 2).Participants);
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task EditCorrectEditAsync()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CourseTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            var originalDate = DateTime.Parse("01/01/2021");
            var editedDate = DateTime.Parse("02/02/2021");
            var courseInicial = new Course()
            {
                Id = 2,
                Description = "description",
                GroupSize = 1,
                Organizer = "organizer",
                Place = "place",
                Title = "title",
                Price = 11,
                LengthInDays = 12,
                ImageUrl = "Url",
                StartDate = originalDate,
            };
            dbContext.Courses.Add(new Course() { Id = 1, Title = "First", });
            dbContext.Courses.Add(courseInicial);
            dbContext.Courses.Add(new Course() { Id = 3, Title = "Third" });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<Course>(courseInicial).State = EntityState.Detached;

            var input = new CourseEditInputModel()
            {
                Id = 2,
                Description = "Edited description",
                GroupSize = 5,
                Organizer = "Edited organizer",
                Place = "Edited place",
                Title = "Edited title",
                Price = 15,
                LengthInDays = 25,
                ImageUrl = "Url edited",
                StartDate = editedDate,
            };

            using var repository = new EfDeletableEntityRepository<Course>(dbContext);
            var service = new CoursesService(repository);
            await service.EditAsync(input);
            var course = dbContext.Courses.Find(2);
            Assert.Equal("Edited description", course.Description);
            Assert.Equal(5, course.GroupSize);
            Assert.Equal("Edited organizer", course.Organizer);
            Assert.Equal("Edited place", course.Place);
            Assert.Equal("Edited title", course.Title);
            Assert.Equal(15, course.Price);
            Assert.Equal(25, course.LengthInDays);
            Assert.Equal("Url edited", course.ImageUrl);
            Assert.Equal(editedDate, course.StartDate);
        }
    }
}
