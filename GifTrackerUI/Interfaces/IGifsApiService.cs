﻿using GifTrackerUI.Models;

namespace GifTrackerUI.Interfaces
{
    public interface IGifsApiService
    {
        Task<List<Gif>> GetGifs();
        Task<bool> CreateGif(Gif gif);
    }
}
