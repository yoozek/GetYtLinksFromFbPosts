using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YtVideosFromFb.Options;

namespace YtVideosFromFb
{
    public class App : IApp
    {
        private readonly IFacebookService _facebookService;
        private readonly ILogger<App> _logger;

        private readonly string _accessToken;
        private readonly string _outputFilePath;
        
        public App(IFacebookService facebookService, ILogger<App> logger, AppConfig appConfig)
        {
            _facebookService = facebookService;
            _logger = logger;
            _accessToken = appConfig.AccessToken;
            _outputFilePath = appConfig.OutputFilePath;
        }
        
        public async Task Run()
        {
            var posts = await GetFacebookPosts();
            var youtubeLinks = ExtractYoutubeLinks(posts);
            await SaveToFile(youtubeLinks);
        }

        private async Task SaveToFile(List<string> youtubeLinks)
        {
            await using TextWriter tw = new StreamWriter(_outputFilePath);
            foreach (var link in youtubeLinks)
            {
                await tw.WriteLineAsync(link);
            }
        }

        private List<string> ExtractYoutubeLinks(List<Post> posts)
        {
            var youtubeLinks = posts
                .Where(p => !string.IsNullOrEmpty(p.link) && p.link.ToLowerInvariant().Contains("youtube"))
                .Select(p => p.link)
                .Distinct()
                .ToList();

            _logger.LogInformation($"Found {youtubeLinks.Count} youtube links");
            return youtubeLinks;
        }

        private async Task<List<Post>> GetFacebookPosts()
        {
            var posts = await _facebookService.GetFeedPosts(_accessToken);
            _logger.LogInformation($"Found {posts.Count} posts");
            return posts;
        }
    }
}