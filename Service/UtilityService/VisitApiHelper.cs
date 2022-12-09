using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UtilityService
{
    public class VisitApiService
    {
        private HttpClient httpClient = new HttpClient();
        public static string? Token;
        public static string? Url;
        public async Task<string> CallApiAsync(string url, string method, string accessToken = null, dynamic entity = null)
        {
            HttpResponseMessage? httpResponse = null;
            if (accessToken != null) httpClient.SetBearerToken(accessToken);
            if (method.ToUpper() == "GET")
                httpResponse = await httpClient.GetAsync(url);
            else if (method.ToUpper() == "POST")
            {
                httpResponse = await httpClient.PostAsync(url,
                    new StringContent(Serialize(entity), System.Text.Encoding.UTF8, "application/json"));
            }
            else if (method.ToUpper() == "PUT")
            {
                httpResponse = await httpClient.PutAsync(url,
                    new StringContent(Serialize(entity), System.Text.Encoding.UTF8, "application/json"));
            }
            else if (method.ToUpper() == "DELETE")
                httpResponse = await httpClient.DeleteAsync(url);
            if (!httpResponse.IsSuccessStatusCode)
                return httpResponse.StatusCode.ToString();
            else
            {
                var result = httpResponse.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        public string Serialize(dynamic entity)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(entity);
        }

        public T DeSerialize<T>(dynamic entity)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(entity);
        }
    }
}
