namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

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

        public async Task EditAsync<T>(T input)
        {
            var item = AutoMapperConfig.MapperInstance.Map<ExpositionItem>(input);
            this.exposicionItems.Update(item);
            await this.exposicionItems.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.exposicionItems.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllAccepted<T>(int page, int itemsPerPage, Category itemCategory = 0, string author = null)
        {
            var result = this.exposicionItems.AllAsNoTracking().Where(x => x.Accepted == true);
            if (itemCategory != 0)
            {
                result = result.Where(x => x.Category == itemCategory);
            }

            if (author != null)
            {
                result = result.Where(x => x.Author.Id == author);
            }

            return result.OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
        }

        public int GetCountAccepted(string authorID = null, Category itemCategory = 0)
        {
            var result = this.exposicionItems.All().Where(x => x.Accepted == true);
            if (authorID != null)
            {
                result = result.Where(x => x.AuthorId == authorID);
            }

            if (itemCategory != 0)
            {
                result = result.Where(x => x.Category == itemCategory);
            }

            return result.Count();
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

        public async Task DeleteAsync(int itemId)
        {
            var item = this.exposicionItems.All().Where(x => x.Id == itemId).FirstOrDefault();
            this.exposicionItems.Delete(item);
            await this.exposicionItems.SaveChangesAsync();
        }

        public T GetLastExpositionItem<T>()
        {
            return this.exposicionItems.All().OrderByDescending(x => x.ModifiedOn).To<T>().FirstOrDefault();
        }
    }
}
