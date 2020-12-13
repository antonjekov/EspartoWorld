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
    using EspartoWorld.Web.ViewModels.Product;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    [Collection("Serial")]
    public class ProductsServiceTests
    {
        private DbContextOptions<ApplicationDbContext> options;

        public ProductsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProductsTestDb").Options;
        }

        [Fact]
        public async Task AddsCorrectAddAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductsService(repository);

            dbContext.Products.Add(new Product() { Name = "Third", CreatedOn = DateTime.Now.AddDays(1) });
            dbContext.Products.Add(new Product() { Name = "First", CreatedOn = DateTime.Now });
            dbContext.Products.Add(new Product() { Name = "Second", CreatedOn = DateTime.Now.AddHours(1) });
            await dbContext.SaveChangesAsync();

            var actual = await service.AddAsync(new ProductInputModel() { Name = "Fourth" });
            Assert.Equal(4, actual);
            Assert.Equal(4, dbContext.Products.Count());
        }

        [Fact]
        public void GetByIdReturnsCorrect()
        {
            var list = new List<Product>()
            {
                new Product() { Id = 3, CreatedOn = DateTime.Now.AddDays(1), Name = "Last", Visible = true },
                new Product() { Id = 1, CreatedOn = DateTime.Now, Name = "First", Visible = true },
                new Product() { Id = 2, CreatedOn = DateTime.Now.AddHours(1), Name = "Second" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Product>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ProductsService(mockRepo.Object);
            var actual = service.GetById<ProductViewModel>(2);
            Assert.Equal("Second", actual.Name);
            actual = service.GetById<ProductViewModel>(9);
            Assert.Null(actual);
        }

        [Fact]
        public void IdIsValidReturnsCorrect()
        {
            var list = new List<Product>()
            {
                new Product() { Id = 3, CreatedOn = DateTime.Now.AddDays(1), Name = "Last", Visible = true },
                new Product() { Id = 1, CreatedOn = DateTime.Now, Name = "First", Visible = true },
                new Product() { Id = 2, CreatedOn = DateTime.Now.AddHours(1), Name = "Second" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Product>>();
            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new ProductsService(mockRepo.Object);
            Assert.True(service.IdIsValid(2));
            Assert.False(service.IdIsValid(4));
        }

        [Fact]
        public void GetAllReturnsCorrect()
        {
            var list = new List<Product>()
            {
                new Product() { Id = 3, CreatedOn = DateTime.Now.AddDays(1), Name = "Last", Visible = true },
                new Product() { Id = 1, CreatedOn = DateTime.Now, Name = "First", Visible = true },
                new Product() { Id = 2, CreatedOn = DateTime.Now.AddHours(1), Name = "Second" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Product>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ProductsService(mockRepo.Object);
            var actual = service.GetAll<ProductViewModel>();
            var expected = new List<ProductViewModel>()
            {
                new ProductViewModel() { Id = 3, Name = "Last" },
                new ProductViewModel() { Id = 1, Name = "First" },
                new ProductViewModel() { Id = 2, Name = "Second" },
            };
            Assert.Equal(expected[0].Name, actual.ToList()[0].Name);
            Assert.Equal(expected[1].Name, actual.ToList()[1].Name);
            Assert.Equal(expected[2].Name, actual.ToList()[2].Name);
        }

        [Fact]
        public void GetAllVisibleOrderedCreatedOnReturnsCorrect()
        {
            var list = new List<Product>()
            {
                new Product() { Id = 3, CreatedOn = DateTime.Now.AddDays(1), Name = "Last", Visible = true },
                new Product() { Id = 1, CreatedOn = DateTime.Now, Name = "First", Visible = true },
                new Product() { Id = 2, CreatedOn = DateTime.Now.AddHours(1), Visible = false, Name = "Second" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Product>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ProductsService(mockRepo.Object);
            var actual = service.GetAllVisibleOrderedCreatedOn<ProductViewModel>();
            var expected = new List<ProductViewModel>()
            {
                new ProductViewModel() { Id = 3, Name = "Last" },
                new ProductViewModel() { Id = 1, Name = "First" },
            };
            Assert.Equal(expected[0].Name, actual.ToList()[0].Name);
            Assert.Equal(expected[1].Name, actual.ToList()[1].Name);
        }

        [Fact]
        public async Task IncreasesIncreaseTimesBoughtAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductsService(repository);

            dbContext.Products.Add(new Product() { Id = 3, Name = "Third", CreatedOn = DateTime.Now.AddDays(1) });
            dbContext.Products.Add(new Product() { Id = 1, Name = "First", CreatedOn = DateTime.Now });
            dbContext.Products.Add(new Product() { Id = 2, Name = "Second", CreatedOn = DateTime.Now.AddHours(1) });
            await dbContext.SaveChangesAsync();

            await service.IncreaseTimesBoughtAsync(2);
            Assert.Equal(1, dbContext.Products.Find(2).TimesBought);
        }
    }
}
