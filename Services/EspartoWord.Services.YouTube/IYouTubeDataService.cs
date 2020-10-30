namespace EspartoWorld.Services.YouTube
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Google.Apis.YouTube.v3.Data;

    public interface IYouTubeDataService
    {
        Task<List<SearchResult>> GetLastVideosAsync(string searchWord, int countResults);
    }
}