using GifTrackerUI.Interfaces;
using GifTrackerUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GifTrackerUI.Controllers
{
    public class GifsController : Controller
    {
        private readonly IGifsApiService _gifsApiService;

        public GifsController(IGifsApiService gifsApiService) 
        { 
            _gifsApiService = gifsApiService;
        }

        public async Task<IActionResult> Index()
        {
            var gifs = new List<Gif>();
            gifs = await _gifsApiService.GetGifs();
            return View(gifs);
        }
    }
}
