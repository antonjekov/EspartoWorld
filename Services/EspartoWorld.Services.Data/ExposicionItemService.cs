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
        private readonly IDeletableEntityRepository<ExposicionItem> exposicionItems;

        public ExposicionItemService(IDeletableEntityRepository<ExposicionItem> exposicionItems)
        {
            this.exposicionItems = exposicionItems;
        }

        public async Task<int> AddAsync<T>(T input)
        {
            var item = AutoMapperConfig.MapperInstance.Map<ExposicionItem>(input);
            await this.exposicionItems.AddAsync(item);
            await this.exposicionItems.SaveChangesAsync();
            return item.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.exposicionItems.All().To<T>().ToList();
        }

        public T GetById<T>(int itemId)
        {
            return this.exposicionItems.All().Where(x => x.Id == itemId).To<T>().FirstOrDefault();
        }
    }
}
