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

        public IActionResult Create()
        {
            return View();
        }
        
       [HttpPost]
        public async Task<IActionResult> Index(Gif gif)
        {
            var result = await _gifsApiService.CreateGif(gif);
            if (result)
            {
                return Redirect("/gifs");
            }
            else
            {
                return Redirect("/gifs/create");
            }
        }
    }
}
