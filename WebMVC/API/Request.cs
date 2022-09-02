using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using System.Text;
using WebMVC.Models.Cons;

namespace WebMVC.API
{
    public class Request : IRequest
    {
        #region Fields
        private readonly IConfiguration _configuration;
        string baseUrl;
        IHttpContextAccessor _httpContextAccessor;
        string? token;
        private readonly HttpClient httpClient;
        #endregion

        #region Ctor
        public Request(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            baseUrl = _configuration.GetSection("ApiBaseUrl").Get<ApiBaseUrl>().Value;
            this.httpClient = httpClient;
        }
        #endregion

        #region CallApiAsync
        public async Task<string> GetAsync(string Url)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = AuthValue();
                httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                using (var response = await httpClient.GetAsync(string.Format(baseUrl + Url)))
                {
                    apiResponse = response.Content.ReadAsStringAsync().Result;
                }
                return apiResponse;
            }
            catch (Exception)
            {
                return Constants.Exception;
            }
        }

        public async Task<string> PostAsync(string Url, object Request)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = AuthValue();

                httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(string.Format(baseUrl + Url), content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }

                return apiResponse;
            }
            catch (Exception)
            {
                return Constants.Exception;
            }
        }

        public async Task<string> PutAsync(string Url, object RequestItem)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = AuthValue();
                httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                StringContent content = new StringContent(JsonConvert.SerializeObject(RequestItem),
                    Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(String.Format(baseUrl + Url), content))
                {
                    apiResponse = response.Content.ReadAsStringAsync().Result;
                }

                return apiResponse;
            }
            catch (Exception)
            {
                return Constants.Exception;
            }
        }

        public async Task<string> DeleteAsync(string Url)
        {
            try
            {
                var apiResponse = "Response Is Null";
                var authValue = AuthValue();
                httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                using (var response = await httpClient.DeleteAsync(String.Format(baseUrl + Url)))
                {
                    apiResponse = response.Content.ReadAsStringAsync().Result;
                }

                return apiResponse;
            }
            catch (Exception)
            {
                return Constants.Exception;
            }

        }


        #endregion
        #region CallApi
        public string Get(string Url)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = AuthValue();
                httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                using (var response = httpClient.GetAsync(string.Format(baseUrl + Url)).Result)
                {
                    apiResponse = response.Content.ReadAsStringAsync().Result;
                }

                return apiResponse;
            }
            catch (Exception)
            {
                return Constants.Exception;
            }

        }
        public string Post(string Url, object Request)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = AuthValue();
                httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                StringContent content = new StringContent(JsonConvert.SerializeObject(Request), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync(string.Format(baseUrl + Url), content).Result)
                {
                    apiResponse = response.Content.ReadAsStringAsync().Result;
                }

                return apiResponse;
            }
            catch (Exception)
            {
                return Constants.Exception;
            }

        }
        public string Delete(string Url)
        {
            try
            {
                string apiResponse = "Response Is Null";
                var authValue = AuthValue();
                httpClient.DefaultRequestHeaders.Add("Authorization", authValue);
                using (var response = httpClient.DeleteAsync(string.Format(baseUrl + Url)).Result)
                {
                    apiResponse = response.Content.ReadAsStringAsync().Result;
                }

                return apiResponse;
            }
            catch (Exception)
            {
                return Constants.Exception;
            }

        }
        #endregion

        #region AuthValue
        private string AuthValue()
        {
            //change header(Authorization:Berarer <token>) each request
            this.token = _httpContextAccessor?.HttpContext?.Request.Cookies[Constants.XAccessToken];
            var authValue = "Bearer " + token;
            return authValue;
        }
        #endregion
    }
}
