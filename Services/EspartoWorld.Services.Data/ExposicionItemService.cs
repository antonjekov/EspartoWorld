namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class ExposicionItemService : IExposicionItemService
    {
        private readonly IDeletableEntityRepository<ExpositionItem> exposicionItems;

        public ExposicionItemService(IDeletableEntityRepository<ExpositionItem> exposicionItems)
        {
            this.exposicionItems = exposicionItems;
        }

        public async Task<int> AddAsync<T>(T input)
        {
            var item = AutoMapperConfig.MapperInstance.Map<ExpositionItem>(input);
            await this.exposicionItems.AddAsync(item);
            await this.exposicionItems.SaveChangesAsync();
            return item.Id;
        }

        public async Task Edit<T>(T input)
        {
            var item = AutoMapperConfig.MapperInstance.Map<ExpositionItem>(input);
            this.exposicionItems.Update(item);
            await this.exposicionItems.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.exposicionItems.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllAccepted<T>(int page, int itemsPerPage)
        {
            return this.exposicionItems.AllAsNoTracking().Where(x => x.Accepted == true).OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
        }

        public IEnumerable<T> GetAllForModerate<T>()
        {
            return this.exposicionItems.All().Where(x => x.Accepted == false).To<T>().ToList();
        }

        public T GetById<T>(int itemId)
        {
            return this.exposicionItems.All().Where(x => x.Id == itemId).To<T>().FirstOrDefault();
        }

        public bool IdIsValid(int id)
        {
            return this.exposicionItems.AllAsNoTracking().Any(x => x.Id == id);
        }

        public async Task Delete(int itemId)
        {
            var item = this.exposicionItems.All().Where(x => x.Id == itemId).FirstOrDefault();
            this.exposicionItems.Delete(item);
            await this.exposicionItems.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllAcceptedByAuthorId<T>(string autorID, int page, int itemsPerPage)
        {
            return this.exposicionItems.AllAsNoTracking().Where(x => x.Accepted == true && x.AuthorId == autorID).OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
        }

        public T GetLastExpositionItem<T>()
        {
            return this.exposicionItems.All().OrderByDescending(x => x.ModifiedOn).To<T>().FirstOrDefault();
        }

        public int GetCountAccepted()
        {
            return this.exposicionItems.All().Where(x => x.Accepted == true).Count();
        }

        public int GetCountAccepted(string authorID)
        {
            return this.exposicionItems.All().Where(x => x.Accepted == true && x.AuthorId == authorID).Count();
        }
    }
}
