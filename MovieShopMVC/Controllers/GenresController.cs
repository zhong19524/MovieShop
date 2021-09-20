using ApplicationCore.Entities;
using ApplicationCore.ServiceInterfaces;
using Infrastrcture.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        private readonly IMovieService _movieService;
        public GenresController(IGenreService genreService, IMovieService movieService)
        {
            _genreService = genreService;
            _movieService = movieService;
        }
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllGenres();
            return View(genres);
        }

        public async Task<IActionResult> MoviesByGenres(int id)
        {
            var movieCards = await _movieService.GetMoviesByGenre(id);
            return View(movieCards);
        }
    }
}
