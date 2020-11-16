namespace EspartoWorld.Services.Data
{
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class ContactFormService : IContactFormService
    {
        private readonly IDeletableEntityRepository<ContactFormMessage> messagesRepository;

        public ContactFormService(IDeletableEntityRepository<ContactFormMessage> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task<int> AddAsync<T>(T input, string ip)
        {
            var message = AutoMapperConfig.MapperInstance.Map<ContactFormMessage>(input);
            message.Ip = ip;
            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
            return message.Id;
        }
    }
}
