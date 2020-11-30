using EspartoWorld.Data.Common.Repositories;
using EspartoWorld.Data.Models;
using EspartoWorld.Web.ViewModels.Videos;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EspartoWorld.Services.Data.Tests
{
    public class VideosServiceTests
    {
        [Fact]
        public void GetByIdReturnsNull()
        {
            var list = new List<Video>();
            var mockRepo = new Mock<IDeletableEntityRepository<Video>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new VideosService(mockRepo.Object);

            var video = service.GetById<VideoViewModel>(2);

            Assert.Null(video);
        }
    }
}
