
namespace WebMVC.API
{
    public interface IRequest
    {
        Task<string> GetAsync(string Url);
        Task<string> PostAsync(string Url, object Request);
        Task<string> PutAsync(string Url, object RequestItem);
        Task<string> DeleteAsync(string Url);

        string Get(string Url);
        string Post(string Url, object Request);
        string Delete(string Url);
    }
}
