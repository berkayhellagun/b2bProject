using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using System.Text;
using WebMVC.Models;
using WebMVC.Models.Response;

namespace WebMVC.API
{
    public class Request : IRequest
    {
        #region Fields
        private readonly IConfiguration _configuration;
        string baseUrl;
        IHttpContextAccessor _httpContextAccessor;
        string? token;
        #endregion

        #region Ctor
        public Request(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            baseUrl = _configuration.GetSection("ApiBaseUrl").Get<ApiBaseUrl>().Value;
            //null geliyor ilk loginden sonra
        }
        #endregion

        private string Value()
        {
            this.token = _httpContextAccessor?.HttpContext?.Request.Cookies[Constants.XAccessToken];
            var authValue = "Bearer " + token;
            return authValue;
        }

        #region CallApiAsync
        public async Task<string> GetAsync(string Url)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = Value();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                    using (var response = await httpClient.GetAsync(string.Format(baseUrl + Url)))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                return apiResponse;
            }
            catch (Exception)
            {
                return baseUrl;
            }
        }

        public async Task<string> PostAsync(string Url, object Request)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = Value();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
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
                return baseUrl;
            }

        }
        #endregion
        #region CallApi
        public string Get(string Url)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = Value();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                    using (var response = httpClient.GetAsync(string.Format(baseUrl + Url)).Result)
                    {
                        apiResponse = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return apiResponse;
            }
            catch (Exception)
            {
                return baseUrl;
            }

        }
        public string Post(string Url, object Request)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = Value();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
                    using (var response = httpClient.PostAsync(string.Format(baseUrl + Url), content).Result)
                    {
                        apiResponse = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return apiResponse;
            }
            catch (Exception)
            {
                return baseUrl + Url;
            }

        }
        public string Delete(string Url)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = Value();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                    using (var response = httpClient.DeleteAsync(string.Format(baseUrl + Url)).Result)
                    {
                        apiResponse = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return apiResponse;
            }
            catch (Exception)
            {
                return baseUrl;
            }

        }
        #endregion
    }
}
