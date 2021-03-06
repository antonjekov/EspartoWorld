﻿namespace EspartoWorld.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<int> AddAsync<T>(T input);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>();

        bool IdIsValid(int id);

        IEnumerable<T> GetAllVisibleOrderedCreatedOn<T>();

        Task IncreaseTimesBoughtAsync(int productId);
    }
}
