using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopMVC.Controllers
{

    // ASP.NET Core Filters allows code to run before or after specific stages
    // Filters
    [Authorize]
    public class UserController : Controller
    {
        // user/GetAllPurchases
        private readonly ICurrentUserService _currentUserService;

        //public UserController(ICurrentUserService currentUserService)
        //{
        //    _currentUserService = currentUserService;
        //}

        private readonly IUserService _userService;
        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }


        public async Task<IActionResult> GetAllPurchases()
        {
            var userId =  _currentUserService.UserId;
            var purchasedMovies = await _userService.GetPurchasedMovies(userId);
            return View(purchasedMovies);
        }



        public async Task<IActionResult> GetFavorites()
        {
            var userId = _currentUserService.UserId;
            var favoriteMovies = await _userService.GetFavorites(userId);
            return View(favoriteMovies);
        }


        public async Task<IActionResult> GetProfile()
        {
            var userId = _currentUserService.UserId;
            var userProfile = await _userService.GetProfile(userId);
            return View(userProfile);
        }


        public async Task<IActionResult> EditProfile()
        {
            return View();
        }


        public async Task<IActionResult> BuyMovie()
        {

            return View();
        }


        public async Task<IActionResult> FavoriteMovie()
        {
            return View();
        }
    }
}
