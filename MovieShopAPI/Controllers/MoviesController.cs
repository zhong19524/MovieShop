using ApplicationCore.Entities;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Exceptions;
namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //The default 
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movie = await _movieService.GetTopRatedMovies();
            if (!movie.Any())
            {
                throw new NotFoundException("Not FOUND");
            }
            return Ok(movie);
        }

        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> GetTopRenueMovies()
        {

            var movie = await _movieService.GetTopRevenueMovies();
            if (!movie.Any())
            {
                throw new NotFoundException("NOT Found");
            }
            // 200 OK
            return Ok(movie);
            //Serialization => object to another type of object
            //C# to JSON
            //

        }

        [Route("toprated")]
        [HttpGet]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movie = await _movieService.GetTopRatedMovies();
            if (!movie.Any())
            {
                throw new NotFoundException("Not FOUND");
            }
            return Ok(movie);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);

            return Ok(movieDetails);
        }

        [Route("genre/{genreid}")]
        [HttpGet]
        public async Task<IActionResult> MoviesByGenre(int genreid)
        {
            var movieCards = await _movieService.GetMoviesByGenre(genreid);
            return Ok(movieCards);
        }

        [Route("{id}/reviews")]
        [HttpGet]
        public async Task<IActionResult> GetTopReviews(int id)
        {
            var reviews = await _movieService.GetTopReviews(id);
            return Ok(reviews);
        }


    }
}
