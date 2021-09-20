using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }
    }
}
