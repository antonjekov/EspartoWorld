namespace EspartoWorld.Services.Data.Tests
{
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
    using EspartoWorld.Web.ViewModels.ShoppingCart;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    [Collection("Serial")]
    public class ShoppingCarsServiceTests
    {
        private DbContextOptions<ApplicationDbContext> options;

        public ShoppingCarsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShoppingCartsTestDb").Options;
        }

        [Fact]
        public void GetByIdReturnsCorrect()
        {
            var list = new List<ShoppingCartItem>()
            {
                new ShoppingCartItem() { Id = 3, ApplicationUserId = "user1" },
                new ShoppingCartItem() { Id = 1, ApplicationUserId = "user1" },
                new ShoppingCartItem() { Id = 2, ApplicationUserId = "user2" },
            };
            var mockRepo = new Mock<IRepository<ShoppingCartItem>>();
            mockRepo.Setup(r => r.All()).Returns(list.AsQueryable());
            var service = new ShoppingCartsService(mockRepo.Object);
            var actual = service.GetAllByUserId<ShoppingCartViewModel>("user1");
            Assert.Equal(2, actual.Count());
            actual = service.GetAllByUserId<ShoppingCartViewModel>("anyId");
            Assert.Empty(actual);
        }

        [Fact]
        public async Task AddsCorrectAddAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfRepository<ShoppingCartItem>(dbContext);
            var service = new ShoppingCartsService(repository);

            var initialShoppingCartItem = new ShoppingCartItem() { Id = 3, ApplicationUserId = "user1", ProductId = 2, Quantity = 3 };
            dbContext.ShoppingCartItems.Add(initialShoppingCartItem);
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { Id = 2, ApplicationUserId = "user1" });
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { Id = 1, ApplicationUserId = "user2" });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<ShoppingCartItem>(initialShoppingCartItem).State = EntityState.Detached;

            await service.AddAsync("user10", 2, 10);
            Assert.Equal(4, dbContext.ShoppingCartItems.Count());
            await service.AddAsync("user1", 2, 10);
            Assert.Equal(4, dbContext.ShoppingCartItems.Count());
            var cart = dbContext.ShoppingCartItems.Find(3);
            Assert.Equal(13, cart.Quantity);
        }

        [Fact]
        public async Task DeletesDeleteAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfRepository<ShoppingCartItem>(dbContext);
            var service = new ShoppingCartsService(repository);

            var initialShoppingCartItem = new ShoppingCartItem() { Id = 3, ApplicationUserId = "user1", ProductId = 2, Quantity = 3 };
            dbContext.ShoppingCartItems.Add(initialShoppingCartItem);
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { Id = 2, ApplicationUserId = "user1", ProductId = 3 });
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { Id = 1, ApplicationUserId = "user2" });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<ShoppingCartItem>(initialShoppingCartItem).State = EntityState.Detached;

            Assert.Equal(3, dbContext.ShoppingCartItems.Count());
            await service.DeleteAsync("user1", 2);
            Assert.Null(dbContext.ShoppingCartItems.Find(3));
            Assert.Equal(2, dbContext.ShoppingCartItems.Count());
            await service.DeleteAsync("user1", 120);
            Assert.Equal(2, dbContext.ShoppingCartItems.Count());
        }

        [Fact]
        public async Task DeletesDeleteAllAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfRepository<ShoppingCartItem>(dbContext);
            var service = new ShoppingCartsService(repository);

            var initialShoppingCartItem1 = new ShoppingCartItem() { Id = 3, ApplicationUserId = "user1", ProductId = 2, Quantity = 3 };
            var initialShoppingCartItem2 = new ShoppingCartItem() { Id = 2, ApplicationUserId = "user1", ProductId = 3 };
            dbContext.ShoppingCartItems.Add(initialShoppingCartItem1);
            dbContext.ShoppingCartItems.Add(initialShoppingCartItem2);
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { Id = 1, ApplicationUserId = "user2" });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<ShoppingCartItem>(initialShoppingCartItem1).State = EntityState.Detached;
            dbContext.Entry<ShoppingCartItem>(initialShoppingCartItem2).State = EntityState.Detached;

            Assert.Equal(3, dbContext.ShoppingCartItems.Count());
            await service.DeleteAllAsync("user1");
            Assert.Null(dbContext.ShoppingCartItems.Find(3));
            Assert.Null(dbContext.ShoppingCartItems.Find(2));
            Assert.Equal(1, dbContext.ShoppingCartItems.Count());
            await service.DeleteAllAsync("user10");
            Assert.Equal(1, dbContext.ShoppingCartItems.Count());
        }

        [Fact]
        public async Task UpdatesUpdateQuantityAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfRepository<ShoppingCartItem>(dbContext);
            var service = new ShoppingCartsService(repository);

            var initialShoppingCartItem = new ShoppingCartItem() { Id = 3, ApplicationUserId = "user1", ProductId = 2, Quantity = 3 };
            dbContext.ShoppingCartItems.Add(initialShoppingCartItem);
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { Id = 2, ApplicationUserId = "user1" });
            dbContext.ShoppingCartItems.Add(new ShoppingCartItem() { Id = 1, ApplicationUserId = "user2" });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<ShoppingCartItem>(initialShoppingCartItem).State = EntityState.Detached;

            await service.UpdateQuantityAsync("user1", 2, 100);
            Assert.Equal(3, dbContext.ShoppingCartItems.Count());
            var cart = dbContext.ShoppingCartItems.Find(3);
            Assert.Equal(100, cart.Quantity);
            await service.UpdateQuantityAsync("user1", 2, 0);
            cart = dbContext.ShoppingCartItems.Find(3);
            Assert.Null(cart);
        }
    }
}
