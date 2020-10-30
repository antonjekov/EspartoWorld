namespace EspartoWorld.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EspartoWorld.Data.Common.Repositories;
    using EspartoWorld.Data.Models;
    using EspartoWorld.Services.Mapping;

    public class VideosService : IVideosService
    {
        private readonly IDeletableEntityRepository<Video> videoRepository;

        public VideosService(IDeletableEntityRepository<Video> videoRepository)
        {
            this.videoRepository = videoRepository;
        }

        public async Task<int> AddAsync<T>(T input)
        {
            var video = AutoMapperConfig.MapperInstance.Map<Video>(input);
            await this.videoRepository.AddAsync(video);
            await this.videoRepository.SaveChangesAsync();
            return video.Id;
        }

        public ICollection<T> GetAll<T>()
        {
            return this.videoRepository.All().To<T>().ToList();
        }

        public T GetById<T>(int videoId)
        {
            return this.videoRepository.All().Where(v => v.Id == videoId).To<T>().FirstOrDefault();
        }

        public T GetLastVideo<T>()
        {
            return this.videoRepository.All().OrderByDescending(v => v.CreatedOn).To<T>().FirstOrDefault();
        }
    }
}
