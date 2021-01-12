namespace EspartoWorld.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using EspartoWorld.Data;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Data.Repositories;
    using EspartoWorld.Services.Mapping;
    using EspartoWorld.Web.ViewModels;
    using EspartoWorld.Web.ViewModels.Contact;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    [Collection("Serial")]
    public class ContactFormService
    {
        [Fact]
        public async Task AddsCorrectAddAsync()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ContactFormMessagesTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.ContactFormMessages.Add(new ContactFormMessage() { Id = 2, Title = "First" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<ContactFormMessage>(dbContext);
            var service = new EspartoWorld.Services.Data.ContactFormService(repository);
            var actual = await service.AddAsync(new ContactFormViewModel() { Title = "Second" }, "someIp");
            Assert.Equal(2, dbContext.ContactFormMessages.Count());
        }
    }
}
