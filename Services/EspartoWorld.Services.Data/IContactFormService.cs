namespace EspartoWorld.Services.Data
{
    using System.Threading.Tasks;

    public interface IContactFormService
    {
        Task<int> AddAsync<T>(T input, string ip);
    }
}
