namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<int> AddAsync<T>(T input)
        {
            var item = AutoMapperConfig.MapperInstance.Map<Product>(input);
            await this.productsRepository.AddAsync(item);
            await this.productsRepository.SaveChangesAsync();
            return item.Id;
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            return this.productsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public bool IdIsValid(int id)
        {
            return this.productsRepository.AllAsNoTracking().Any(x => x.Id == id);
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.productsRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllVisibleOrderedCreatedOn<T>()
        {
            return this.productsRepository.All().Where(x => x.Visible).OrderByDescending(x => x.CreatedOn).To<T>().ToList();
        }
    }
}
