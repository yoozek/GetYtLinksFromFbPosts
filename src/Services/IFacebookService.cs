using System.Collections.Generic;
using System.Threading.Tasks;
using YtVideosFromFb;

public interface IFacebookService
{
    Task<List<Post>> GetFeedPosts(string accessToken, string nextPageUrl = null, List<Post> posts = null);
}