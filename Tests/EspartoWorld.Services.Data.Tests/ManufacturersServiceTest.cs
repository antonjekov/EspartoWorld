using EspartoWorld.Data;
using EspartoWorld.Data.Common.Repositories;
using EspartoWorld.Data.Models;
using EspartoWorld.Data.Repositories;
using EspartoWorld.Services.Mapping;
using EspartoWorld.Web.ViewModels;
using EspartoWorld.Web.ViewModels.Manufacturers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EspartoWorld.Services.Data.Tests
{
    public class ManufacturersServiceTest
    {
        private DbContextOptions<ApplicationDbContext> options;

        public ManufacturersServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ManufacturersTestDb").Options;
        }

        [Fact]
        public async Task AddsCorrectAddAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Manufacturer>(dbContext);
            var service = new ManufacturersService(repository);

            dbContext.Manufacturers.Add(new Manufacturer() { Id = "123456789", Name = "Existing" });
            await dbContext.SaveChangesAsync();

            var actual = await service.AddAsync(new ManufacturerInputModel() { Id = "223456789", Name = "Second" });
            Assert.True(dbContext.Manufacturers.Any(x => x.Name == "Second"));
            Assert.Equal(2, dbContext.Manufacturers.Count());
        }

        [Fact]
        public async Task IdExistsReturnsCorrect()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfDeletableEntityRepository<Manufacturer>(dbContext);
            var service = new ManufacturersService(repository);

            dbContext.Manufacturers.Add(new Manufacturer() { Id = "123456789", Name = "Existing" });
            dbContext.Manufacturers.Add(new Manufacturer() { Id = "223456789", Name = "Second" });
            await dbContext.SaveChangesAsync();

            Assert.True(service.IdExists("223456789"));
            Assert.False(service.IdExists("123"));
        }

        [Fact]
        public void GetAllReturnsCorrect()
        {
            var list = new List<Manufacturer>()
            {
                new Manufacturer() { Id = "123456789", Name = "B" },
                new Manufacturer() { Id = "223456789", Name = "A" },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Manufacturer>>();
            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new ManufacturersService(mockRepo.Object);
            var actual = service.GetAll<ManufacturersViewModel>().ToList();
            var expected = new List<ManufacturersViewModel>()
            {
                new ManufacturersViewModel() { Name = "A" },
                new ManufacturersViewModel() { Name = "B" },
            };
            Assert.Equal(expected[0].Name, actual[0].Name);
            Assert.Equal(expected[1].Name, actual[1].Name);
        }

    }
}
