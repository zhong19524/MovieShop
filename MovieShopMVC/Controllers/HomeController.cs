using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastrcture.Services;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Exceptions;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        
        private IMovieService _movieService;

        //IMovieService m = new MovieService();
        //3 ways to 
        //Constuctor Injection
        //Method Injection
        //Property Injection
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task <IActionResult> Index()
        {
            //throw new ConflictException("Not Found");
            // Question
            // which one is better do async/await here or in MovieService
            // (since we are accessing database from there)
            //var GetMoviesTask = Task.Run(() => _movieService.GetTopRevenueMovies());
            //var movies = await GetMoviesTask;


            var movies =  await _movieService.GetTopRevenueMovies();
            // 3 ways to pass data from controller to views
            // 1.Strongly Typed Models
            // 2.ViewBag
            // 3.ViewData
            //ViewBag.PageTitle = "TOP Revenue Movies";
            //ViewData["TotalMovies"] = movies.Count();

            return View(movies);
        }

        public IActionResult Purchased()
        {
            return LocalRedirect("~/User/GetAllPurchases");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
