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
    using EspartoWorld.Web.ViewModels.Videos;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class VideosServiceTests
    {
        private DbContextOptions<ApplicationDbContext> options;

        public VideosServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "VideosTestDb").Options;
        }

        [Fact]
        public void GetByIdReturnsNull()
        {
            var list = new List<Video>();
            var mockRepo = new Mock<IDeletableEntityRepository<Video>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new VideosService(mockRepo.Object);
            var video = service.GetById<VideoViewModel>(2);
            Assert.Null(video);
        }

        [Fact]
        public void GetLastVideoReturnsCorrectVideo()
        {
            var list = new List<Video>()
            {
                new Video() { CreatedOn = DateTime.Now.AddDays(1), Title = "Last video" },
                new Video() { CreatedOn = DateTime.Now, Title = "First video" },
                new Video() { CreatedOn = DateTime.Now.AddHours(1), Title = "Second video" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Video>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new VideosService(mockRepo.Object);
            var actual = service.GetLastVideo<VideoViewModel>();
            Assert.Equal("Last video", actual.Title);
        }

        [Fact]
        public void GetAllReturnsCorrect()
        {
            var list = new List<Video>()
            {
                new Video() { CreatedOn = DateTime.Now.AddDays(1), Title = "Last video" },
                new Video() { CreatedOn = DateTime.Now, Title = "First video" },
                new Video() { CreatedOn = DateTime.Now.AddHours(1), Title = "Second video" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Video>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new VideosService(mockRepo.Object);
            var actual = service.GetAll<VideoViewModel>().ToList();
            var expected = new List<VideoViewModel>()
            {
                new VideoViewModel() { Title = "Last video" },
                new VideoViewModel() { Title = "Second video" },
                new VideoViewModel() { Title = "First video" },
            };
            Assert.Equal(expected[0].Title, actual[0].Title);
            Assert.Equal(expected[1].Title, actual[1].Title);
            Assert.Equal(expected[2].Title, actual[2].Title);
        }

        [Fact]
        public void GetAllWithPaginationReturnsCorrect()
        {
            var list = new List<Video>()
            {
                new Video() { CreatedOn = DateTime.Now.AddDays(1), Title = "Last video" },
                new Video() { CreatedOn = DateTime.Now, Title = "First video" },
                new Video() { CreatedOn = DateTime.Now.AddHours(1), Title = "Second video" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Video>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new VideosService(mockRepo.Object);
            var actual = service.GetAll<VideoViewModel>(2, 2).ToList();
            var expected = new List<VideoViewModel>()
            {
                new VideoViewModel() { Title = "Last video" },
                new VideoViewModel() { Title = "Second video" },
                new VideoViewModel() { Title = "First video" },
            };
            Assert.Single(actual);
            Assert.Equal("First video", actual[0].Title);
        }

        [Fact]
        public void GetCountAllVideosReturnCorrectNumber()
        {
            var list = new List<Video>()
            {
                new Video(),
                new Video(),
                new Video(),
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Video>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new VideosService(mockRepo.Object);
            Assert.Equal(3, service.GetCountAllVideos());
            mockRepo.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task AddsCorrectAddAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Video>(dbContext);
            var service = new VideosService(repository);

            dbContext.Videos.Add(new Video() { Title = "Existing" });
            await dbContext.SaveChangesAsync();

            var actual = await service.AddAsync(new VideoInputModel() { Title = "Second" });
            Assert.Equal(2, actual);
            Assert.Equal(2, dbContext.Videos.Count());
        }

        [Fact]
        public async Task DeletesDeleteAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Video>(dbContext);
            var service = new VideosService(repository);

            dbContext.Videos.Add(new Video() { Title = "First" });
            dbContext.Videos.Add(new Video() { Title = "Second" });
            dbContext.Videos.Add(new Video() { Title = "Third" });
            await dbContext.SaveChangesAsync();

            Assert.Equal(3, dbContext.Videos.Count());
            await service.DeleteAsync(3);
            Assert.Equal(2, dbContext.Videos.Count());
            await Assert.ThrowsAsync<NullReferenceException>(() => service.DeleteAsync(5));
            Assert.Equal(2, dbContext.Videos.Count());
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task EditCorrectEditAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            var videoInicial = new Video()
            {
                Id = 2,
                Title = "title",
            };
            dbContext.Videos.Add(new Video() { Id = 1, Title = "First", });
            dbContext.Videos.Add(videoInicial);
            dbContext.Videos.Add(new Video() { Id = 3, Title = "Third" });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<Video>(videoInicial).State = EntityState.Detached;

            var input = new VideoEditInputViewModel()
            {
                Id = 2,
                Title = "Edited title",
                VideoId = "abv",
            };

            using var repository = new EfDeletableEntityRepository<Video>(dbContext);
            var service = new VideosService(repository);
            await service.EditAsync(input);
            var video = dbContext.Videos.Find(2);
            Assert.Equal("Edited title", video.Title);
            Assert.Equal("abv", video.VideoId);
        }
    }
}
