using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EspartoWorld.Services.Data
{
    public interface IProductsService
    {
        Task<int> AddAsync<T>(T input);

        IEnumerable<T> GetAll<T>();
    }
}
