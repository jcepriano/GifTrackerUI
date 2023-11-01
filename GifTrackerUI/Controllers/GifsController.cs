using GifTrackerUI.Interfaces;
using GifTrackerUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        [Route("gifs/{id:int}")]
        public async Task<IActionResult> Update(int id)
        {
            var gifs = await _gifsApiService.GetGifs();
            var gif = gifs.FirstOrDefault(g => g.Id == id);
            return View(gif);
        }

        [HttpPost]
        [Route("gifs/{id:int}")]
        public async Task<IActionResult> Update(int id, Gif gif)
        {
            var result = await _gifsApiService.UpdateGif(id, gif);
            if (result)
            {
                return Redirect("/gifs");
            }
            else
            {
                return Redirect("/gifs/create");
            }
        }

        [HttpPost]
        [Route("gifs/{id:int}/delete")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _gifsApiService.DeleteGif(id);
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
