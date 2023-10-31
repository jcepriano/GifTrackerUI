using GifTrackerUI.Interfaces;
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

        public IActionResult Index()
        {
            var gifs = _gifsApiService.GetGifs();
            return View(gifs);
        }
    }
}
