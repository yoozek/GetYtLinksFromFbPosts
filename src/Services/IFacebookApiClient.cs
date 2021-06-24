using System.Threading.Tasks;

public interface IFacebookApiClient
{
    Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);
}