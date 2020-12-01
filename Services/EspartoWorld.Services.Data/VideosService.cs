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

        public async Task DeleteAsync(int videoId)
        {
            var video = this.videoRepository.All().Where(x => x.Id == videoId).FirstOrDefault();
            this.videoRepository.Delete(video);
            await this.videoRepository.SaveChangesAsync();
        }

        public ICollection<T> GetAll<T>()
        {
            return this.videoRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToList();
        }

        public ICollection<T> GetAll<T>(int page, int itemsPerPage)
        {
            return this.videoRepository.All().OrderByDescending(x => x.CreatedOn).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();
        }

        public int GetCountAllVideos()
        {
            return this.videoRepository.All().Count();
        }

        public T GetById<T>(int videoId)
        {
            return this.videoRepository.All().Where(v => v.Id == videoId).To<T>().FirstOrDefault();
        }

        public T GetLastVideo<T>()
        {
            return this.videoRepository.All().OrderByDescending(v => v.CreatedOn).To<T>().FirstOrDefault();
        }

        public async Task EditAsync<T>(T input)
        {
            var item = AutoMapperConfig.MapperInstance.Map<Video>(input);
            this.videoRepository.Update(item);
            await this.videoRepository.SaveChangesAsync();
        }
    }
}
