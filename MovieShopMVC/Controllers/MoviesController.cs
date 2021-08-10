using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult MovieDetails(int id)
        {
            return View();
        }
    }
}
