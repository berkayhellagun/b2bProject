using Newtonsoft.Json;
using System.Text;

namespace WebMVC.API
{
    public class Request : IRequest
    {
        #region Fields
        private readonly IConfiguration _configuration;

        #endregion

        #region Ctor
        public Request(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region CallApiAsync
        public async Task<string> GetAsync(string Url)
        {
            try
            {
                var baseUrl = _configuration.GetSection("ApiBaseUrl").Get<ApiBaseUrl>();
                var configuration = new ConfigurationBuilder();
                string apiResponse = "Response Is Null";
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(string.Format(baseUrl + Url)))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                return apiResponse;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> PostAsync(string Url, object Request)
        {
            try
            {
                var baseUrl = _configuration.GetSection("ApiBaseUrl").Get<ApiBaseUrl>();
                string apiResponse = "Response Is Null";
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(string.Format(baseUrl + Url), content))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                return apiResponse;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
        #region CallApi
        public string Get(string Url)
        {
            try
            {
                var baseUrl = _configuration.GetSection("ApiBaseUrl").Get<ApiBaseUrl>().Value;
                

                string apiResponse = "Response Is Null";
                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.GetAsync(string.Format(baseUrl + Url)).Result)
                    {
                        apiResponse = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return apiResponse;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string Post(string Url, object Request)
        {
            try
            {
                var baseUrl = _configuration.GetSection("ApiBaseUrl").Get<ApiBaseUrl>();
                string apiResponse = "Response Is Null";
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
                    using (var response = httpClient.PostAsync(string.Format(baseUrl + Url), content).Result)
                    {
                        apiResponse = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return apiResponse;
            }
            catch (Exception e)
            {

                throw;
            }

        }
        public string Delete(string Url)
        {
            try
            {
                var baseUrl = _configuration.GetSection("ApiBaseUrl").Get<ApiBaseUrl>();
                string apiResponse = "Response Is Null";
                using (var httpClient = new HttpClient())
                {
                    using (var response = httpClient.DeleteAsync(string.Format(baseUrl + Url)).Result)
                    {
                        apiResponse = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return apiResponse;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
    }
}
