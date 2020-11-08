// <copyright file="IYouTubeDataService.cs" company="EspartoWorld">
// Copyright (c) EspartoWorld. All Rights Reserved.
// </copyright>

namespace EspartoWorld.Services.YouTube
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Google.Apis.YouTube.v3.Data;

#pragma warning disable SA1600 // Elements should be documented
    public interface IYouTubeDataService
#pragma warning restore SA1600 // Elements should be documented
    {
        Task<List<SearchResult>> GetLastVideosAsync(string searchWord, int countResults);
    }
}
