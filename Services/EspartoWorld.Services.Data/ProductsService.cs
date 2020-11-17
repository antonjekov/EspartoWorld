namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
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

        public IEnumerable<T> GetAll<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
