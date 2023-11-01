using GifTrackerUI.Models;

namespace GifTrackerUI.Interfaces
{
    public interface IGifsApiService
    {
        Task<List<Gif>> GetGifs();
        Task<bool> CreateGif(Gif gif);
        Task<bool> UpdateGif(int id, Gif gif);
        Task<bool> DeleteGif(int id);
    }
}
