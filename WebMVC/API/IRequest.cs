namespace WebMVC.API
{
    public interface IRequest
    {
        Task<string> GetAsync(string Url);

        string Get(string Url);

        Task<string> PostAsync(string Url, object Request);

        string Post(string Url, object Request);

        string Delete(string Url);
    }
}
