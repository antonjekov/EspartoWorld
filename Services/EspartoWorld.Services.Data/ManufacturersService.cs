namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ManufacturersService : IManufacturersService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturers;

        public ManufacturersService(IDeletableEntityRepository<Manufacturer> manufacturers)
        {
            this.manufacturers = manufacturers;
        }

        public async Task<string> AddAsync<T>(T input)
        {
            var item = AutoMapperConfig.MapperInstance.Map<Manufacturer>(input);
            await this.manufacturers.AddAsync(item);
            await this.manufacturers.SaveChangesAsync();
            return item.Id;
        }

        public bool IdExists(string id)
        {
            return this.manufacturers.AllAsNoTracking().Any(x => x.Id == id);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.manufacturers.AllAsNoTracking().OrderBy(x => x.Name).To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            return this.manufacturers.AllAsNoTracking().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }
    }
}
