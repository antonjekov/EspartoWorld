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
    using EspartoWorld.Web.ViewModels.ExposicionItems;
    using EspartoWorld.Web.ViewModels.ExpositionItems;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    [Collection("Serial")]
    public class ExposicionItemServiceTests
    {
        private DbContextOptions<ApplicationDbContext> options;

        public ExposicionItemServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ExpositionItemsTestDb").Options;
        }

        [Fact]
        public async Task AddsCorrectAddAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<ExpositionItem>(dbContext);
            var service = new ExposicionItemService(repository);

            dbContext.ExpositionItems.Add(new ExpositionItem() { Title = "Existing" });
            await dbContext.SaveChangesAsync();

            var actual = await service.AddAsync(new ExpositionItemInputModel() { Title = "Second" });
            Assert.Equal(2, actual);
            Assert.Equal(2, dbContext.ExpositionItems.Count());
        }

        [Fact]
        public async Task EditCorrectEditAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            var itemInicial = new ExpositionItem()
            {
                Id = 2,
                Title = "title",
            };
            dbContext.ExpositionItems.Add(new ExpositionItem() { Id = 1, Title = "First", });
            dbContext.ExpositionItems.Add(itemInicial);
            dbContext.ExpositionItems.Add(new ExpositionItem() { Id = 3, Title = "Third" });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<ExpositionItem>(itemInicial).State = EntityState.Detached;

            var input = new ExpositionItemModerateModel()
            {
                Id = 2,
                Title = "Edited title",
            };

            using var repository = new EfDeletableEntityRepository<ExpositionItem>(dbContext);
            var service = new ExposicionItemService(repository);
            await service.EditAsync(input);
            var item = dbContext.ExpositionItems.Find(2);
            Assert.Equal("Edited title", item.Title);
        }

        [Fact]
        public void GetAllAcceptedReturnsCorrect()
        {
            var list = new List<ExpositionItem>()
            {
                new ExpositionItem() { CreatedOn = DateTime.Now.AddDays(1), Title = "Last", Accepted = true },
                new ExpositionItem() { CreatedOn = DateTime.Now, Title = "First", Accepted = true },
                new ExpositionItem() { CreatedOn = DateTime.Now.AddHours(1), Title = "Second" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<ExpositionItem>>();
            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new ExposicionItemService(mockRepo.Object);
            var actual = service.GetAllAccepted<ExpositionItemViewModel>(1, 2).ToList();
            var expected = new List<ExpositionItemViewModel>()
            {
                new ExpositionItemViewModel() { Title = "Last" },
                new ExpositionItemViewModel() { Title = "First" },
            };
            Assert.Equal(expected[0].Title, actual[0].Title);
            Assert.Equal(expected[1].Title, actual[1].Title);
        }

        [Fact]
        public void GetCountAcceptedReturnCorrect()
        {
            var list = new List<ExpositionItem>()
            {
                new ExpositionItem() { CreatedOn = DateTime.Now.AddDays(1), Title = "Last", Accepted = true },
                new ExpositionItem() { CreatedOn = DateTime.Now, Title = "First", Accepted = true, Category = Category.Cestas, AuthorId = "anyId" },
                new ExpositionItem() { CreatedOn = DateTime.Now.AddHours(1), Title = "Second", Accepted = true, Category = Category.Cestas },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<ExpositionItem>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ExposicionItemService(mockRepo.Object);
            var actual = service.GetCountAccepted("anyId", Category.Cestas);
            Assert.Equal(1, actual);
            actual = service.GetCountAccepted(itemCategory: Category.Cestas);
            Assert.Equal(2, actual);
        }

        [Fact]
        public void GetAllForModerateReturnCorrect()
        {
            var list = new List<ExpositionItem>()
            {
                new ExpositionItem() { CreatedOn = DateTime.Now.AddDays(1), Title = "Last", Accepted = true },
                new ExpositionItem() { CreatedOn = DateTime.Now, Title = "First", Accepted = true, Category = Category.Cestas, AuthorId = "anyId" },
                new ExpositionItem() { CreatedOn = DateTime.Now.AddHours(1), Title = "Second", Category = Category.Cestas },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<ExpositionItem>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ExposicionItemService(mockRepo.Object);
            var actual = service.GetAllForModerate<ExpositionItemViewModel>();
            Assert.Single(actual);
            Assert.Equal("Second", actual.ToList()[0].Title);
        }

        [Fact]
        public void GetByIdReturnsCorrect()
        {
            var list = new List<ExpositionItem>()
            {
                new ExpositionItem() { Id = 3, CreatedOn = DateTime.Now.AddDays(1), Title = "Last", Accepted = true },
                new ExpositionItem() { Id = 1, CreatedOn = DateTime.Now, Title = "First", Accepted = true, Category = Category.Cestas, AuthorId = "anyId" },
                new ExpositionItem() { Id = 2, CreatedOn = DateTime.Now.AddHours(1), Title = "Second", Category = Category.Cestas },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<ExpositionItem>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ExposicionItemService(mockRepo.Object);
            var actual = service.GetById<ExpositionItemViewModel>(2);
            Assert.Equal("Second", actual.Title);
            actual = service.GetById<ExpositionItemViewModel>(9);
            Assert.Null(actual);
        }

        [Fact]
        public void IdIsValidReturnsCorrect()
        {
            var list = new List<ExpositionItem>()
            {
                new ExpositionItem() { Id = 3, CreatedOn = DateTime.Now.AddDays(1), Title = "Last", Accepted = true },
                new ExpositionItem() { Id = 1, CreatedOn = DateTime.Now, Title = "First", Accepted = true, Category = Category.Cestas, AuthorId = "anyId" },
                new ExpositionItem() { Id = 2, CreatedOn = DateTime.Now.AddHours(1), Title = "Second", Category = Category.Cestas },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<ExpositionItem>>();
            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new ExposicionItemService(mockRepo.Object);
            Assert.True(service.IdIsValid(2));
            Assert.False(service.IdIsValid(4));
        }

        [Fact]
        public async Task DeletesDeleteAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<ExpositionItem>(dbContext);
            var service = new ExposicionItemService(repository);

            dbContext.ExpositionItems.Add(new ExpositionItem() { Id = 3, CreatedOn = DateTime.Now.AddDays(1), Title = "Last", Accepted = true });
            dbContext.ExpositionItems.Add(new ExpositionItem() { Id = 1, CreatedOn = DateTime.Now, Title = "First", Accepted = true, Category = Category.Cestas, AuthorId = "anyId" });
            dbContext.ExpositionItems.Add(new ExpositionItem() { Id = 2, CreatedOn = DateTime.Now.AddHours(1), Title = "Second", Category = Category.Cestas });
            await dbContext.SaveChangesAsync();

            Assert.Equal(3, dbContext.ExpositionItems.Count());
            await service.DeleteAsync(3);
            Assert.Equal(2, dbContext.ExpositionItems.Count());
            Assert.False(dbContext.ExpositionItems.Any(x => x.Title == "Last"));
            await Assert.ThrowsAsync<NullReferenceException>(() => service.DeleteAsync(5));
            Assert.Equal(2, dbContext.ExpositionItems.Count());
        }

        [Fact]
        public void GetLastExpositionItemReturnsCorrect()
        {
            var list = new List<ExpositionItem>()
            {
                new ExpositionItem() { Id = 3, ModifiedOn = DateTime.Now.AddDays(1), Title = "Last" },
                new ExpositionItem() { Id = 1, ModifiedOn = DateTime.Now, Title = "First", Accepted = true, Category = Category.Cestas, AuthorId = "anyId" },
                new ExpositionItem() { Id = 2, ModifiedOn = DateTime.Now.AddHours(1), Title = "Second", Category = Category.Cestas },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<ExpositionItem>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ExposicionItemService(mockRepo.Object);
            var actual = service.GetLastExpositionItem<ExpositionItemViewModel>();
            Assert.Equal("First", actual.Title);
        }

        [Fact]
        public void GetLastExpositionItemReturnsNull()
        {
            var list = new List<ExpositionItem>()
            {
                new ExpositionItem() { Id = 3, ModifiedOn = DateTime.Now.AddDays(1), Title = "Last" },
                new ExpositionItem() { Id = 1, ModifiedOn = DateTime.Now, Title = "First", Category = Category.Cestas, AuthorId = "anyId" },
                new ExpositionItem() { Id = 2, ModifiedOn = DateTime.Now.AddHours(1), Title = "Second", Category = Category.Cestas },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<ExpositionItem>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ExposicionItemService(mockRepo.Object);
            var actual = service.GetLastExpositionItem<ExpositionItemViewModel>();
            Assert.Null(actual);
        }
    }
}
