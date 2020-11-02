namespace EspartoWorld.Services.YouTube
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.Services;
    using Google.Apis.YouTube.v3;
    using Google.Apis.YouTube.v3.Data;

    public class YouTubeDataService : IYouTubeDataService
    {
        private readonly YouTubeService youTubeDateService;

        public YouTubeDataService(string apiKey)
        {
            this.youTubeDateService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString(),
            });
        }

        public async Task<List<SearchResult>> GetLastVideosAsync(string searchWord, int countResults)
        {
            var searchListRequest = this.youTubeDateService.Search.List("snippet");
            searchListRequest.Q = searchWord; // Replace with your search term.
            searchListRequest.MaxResults = countResults;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<SearchResult> videos = new List<SearchResult>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(searchResult);
                        break;
                }
            }

            return videos.OrderByDescending(v => v.Snippet.PublishedAt).ToList();
        }
    }
}
