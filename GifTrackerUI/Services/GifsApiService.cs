using GifTrackerUI.Interfaces;
using GifTrackerUI.Models;
using System.Text;
using System.Text.Json;

namespace GifTrackerUI.Services
{
    public class GifsApiService : IGifsApiService
    {
        private readonly HttpClient _httpClient;

        public GifsApiService(IHttpClientFactory clientFactory) 
        {
            _httpClient = clientFactory.CreateClient("GifsApi");
        }

        public async Task<List<Gif>> GetGifs()
        {
            var url = string.Format("/gifs");
            var result = new List<Gif>();
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<List<Gif>>(stringResponse, 
                    new JsonSerializerOptions() 
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;
        }

        public async Task<bool> CreateGif(Gif gif)
        {
            var url = string.Format("/gifs");
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(gif),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateGif(int id, Gif gif)
        {
            var url = string.Format("/gifs/{0}", id);
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(gif),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGif(int id)
        {
            var url = string.Format("/gifs/{0}", id);
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
