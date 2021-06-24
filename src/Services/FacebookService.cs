using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YtVideosFromFb;
using YtVideosFromFb.Options;

public class FacebookService : IFacebookService
{
    private readonly IFacebookApiClient _facebookApiClient;
    private readonly ILogger<FacebookService> _logger;

    public FacebookService(IFacebookApiClient facebookApiClient, ILogger<FacebookService> logger)
    {
        _facebookApiClient = facebookApiClient;
        _logger = logger;
    }

    public async Task<List<Post>> GetFeedPosts(string accessToken, string nextPageUrl = null, List<Post> posts = null)
    {
        posts ??= new List<Post>();
        
        Feed result;
        if (nextPageUrl == null)
        {
            _logger.LogInformation("Querying https://graph.facebook.com/v11.0/me/feed");
            result = await _facebookApiClient.GetAsync<Feed>(accessToken, "https://graph.facebook.com/v11.0/me/feed", "fields=link,created_time");
        }
        else
        {
            _logger.LogInformation($"Querying {nextPageUrl}");
            result = await _facebookApiClient.GetAsync<Feed>(accessToken, nextPageUrl, "fields=link,created_time");
        }
        
        if (result?.Data != null)
        {
            posts.AddRange(result.Data);
        }
        
        if (!string.IsNullOrEmpty(result?.Paging?.next))
        {
            return await GetFeedPosts(accessToken, result.Paging.next, posts);
        }

        return posts;
    }
}