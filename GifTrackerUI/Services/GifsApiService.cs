using GifTrackerUI.Interfaces;
using GifTrackerUI.Models;
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
    }
}
