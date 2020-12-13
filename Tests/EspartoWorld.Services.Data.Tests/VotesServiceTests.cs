namespace EspartoWorld.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    [Collection("Serial")]
    public class VotesServiceTests
    {
        private DbContextOptions<ApplicationDbContext> options;

        public VotesServiceTests()
        {
            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb").Options;
        }

        [Fact]
        public async Task AddsCorrectAddAsync()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfRepository<Vote>(dbContext);
            var service = new VotesService(repository);

            var vote1 = new Vote() { Id = 1, UserId = "user1", ExpositionItemId = 2, Value = 3 };
            dbContext.Votes.Add(vote1);
            dbContext.Votes.Add(new Vote() { Id = 2, UserId = "user2", ExpositionItemId = 2, Value = 5 });
            dbContext.Votes.Add(new Vote() { Id = 3, UserId = "user1", ExpositionItemId = 1, Value = 5 });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<Vote>(vote1).State = EntityState.Detached;

            await service.SetVoteAsync(3, "user1", 5);
            Assert.Equal(4, dbContext.Votes.Count());
            await service.SetVoteAsync(2, "user1", 5);
            Assert.Equal(4, dbContext.Votes.Count());
            var vote = dbContext.Votes.Find(1);
            Assert.Equal(5, vote.Value);
        }

        [Fact]
        public async Task GetAverageVotesReturnsCorrect()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfRepository<Vote>(dbContext);
            var service = new VotesService(repository);

            var vote1 = new Vote() { Id = 1, UserId = "user1", ExpositionItemId = 2, Value = 3 };
            dbContext.Votes.Add(vote1);
            dbContext.Votes.Add(new Vote() { Id = 2, UserId = "user2", ExpositionItemId = 2, Value = 4 });
            dbContext.Votes.Add(new Vote() { Id = 3, UserId = "user1", ExpositionItemId = 1, Value = 5 });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<Vote>(vote1).State = EntityState.Detached;

            var actual = service.GetAverageVotes(2);
            Assert.Equal(3.5, actual);
            actual = service.GetAverageVotes(10);
            Assert.Equal(0, actual);
        }

        [Fact]
        public async Task GetVotesCountReturnsCorrect()
        {
            using var dbContext = new ApplicationDbContext(this.options);
            dbContext.Database.EnsureDeleted();
            using var repository = new EfRepository<Vote>(dbContext);
            var service = new VotesService(repository);

            var vote1 = new Vote() { Id = 1, UserId = "user1", ExpositionItemId = 2, Value = 3 };
            dbContext.Votes.Add(vote1);
            dbContext.Votes.Add(new Vote() { Id = 2, UserId = "user2", ExpositionItemId = 2, Value = 4 });
            dbContext.Votes.Add(new Vote() { Id = 3, UserId = "user1", ExpositionItemId = 1, Value = 5 });
            await dbContext.SaveChangesAsync();
            dbContext.Entry<Vote>(vote1).State = EntityState.Detached;

            var actual = service.GetVotesCount(2);
            Assert.Equal(2, actual);
            actual = service.GetVotesCount(10);
            Assert.Equal(0, actual);
        }
    }
}
