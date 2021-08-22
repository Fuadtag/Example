using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Utils
{
    public sealed class HttpRequestHelper
    {
        public static async Task<HttpResponseMessage> GetAsync(string url)
        {
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.GetAsync(url);
            httpResponse.EnsureSuccessStatusCode();

            return httpResponse;
        }

        public static async Task<T> DeserializeAsync<T>(HttpResponseMessage responseMessage)
        {
            var response = await responseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(response);
        }

        public static async Task<T> GetAndDeserializeAsync<T>(string url)
        {
            var httpResponse = await GetAsync(url);
            return await DeserializeAsync<T>(httpResponse);
        }
    }
}