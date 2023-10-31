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
            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(gif), Encoding.UTF8, "application/json");
            var gifData = new Dictionary<string, string>
            {
                {"id", "0" },
                {"name", gif.Name },
                {"url", gif.Url },
                {"rating", gif.Rating.ToString() }
            };
            var gifFormData = new FormUrlEncodedContent(gifData);
            var response = await _httpClient.PostAsync("/gifs", content);
            return response.IsSuccessStatusCode;
        }
    }
}
