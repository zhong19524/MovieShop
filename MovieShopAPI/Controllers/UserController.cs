using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        //private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        private readonly IPurchaseService _purchaseService;

        private readonly IFavoriteService _favoriteService;

        private readonly IReviewService _reviewService;
        public UserController(ICurrentUserService currentUserService,IUserService userService, IPurchaseService purchaseService, IFavoriteService favoriteService,IReviewService reviewService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
            _purchaseService = purchaseService;
            _favoriteService = favoriteService;
            _reviewService = reviewService;
        }

        [Authorize]
        [Route("purchase")]
        [HttpPost]
        public async Task<IActionResult> Purchase([FromBody] PurchaseRequestModel model)
        {
            var userId = _currentUserService.UserId;
            if (model.UserId == userId)
            {
                throw new ConflictException("Access Declind");
            }
            var purchase = await _purchaseService.Purchase(model);
            return Ok(purchase);
          
        }

        [Authorize]
        [Route("favorite")]
        [HttpPost]
        public async Task<IActionResult> Favorite([FromBody] FavoriteRequestModel model)
        {
            var userId = _currentUserService.UserId;
            if (model.UserId == userId)
            {
                throw new ConflictException("Access Declind");
            }
            var favorite = await _favoriteService.Favorite(model);
            return Ok(favorite);
        }

        [Authorize]
        [Route("unfavorite")]
        [HttpPost]
        public async Task<IActionResult> UnFavorite([FromBody] FavoriteRequestModel model)
        {
            var userId = _currentUserService.UserId;
            if (model.UserId == userId)
            {
                throw new ConflictException("Access Declind");
            }
            var unfavorite = await _favoriteService.UnFavorite(model);
            return Ok(unfavorite);
        }

        [Authorize]
        [Route("review")]
        [HttpPost]
        public async Task<IActionResult> Review([FromBody] ReviewRequestModel model)
        {
            var userId = _currentUserService.UserId;
            if (model.UserId == userId)
            {
                throw new ConflictException("Access Declind");
            }
            var review = await _reviewService.Review(model);
            return Ok(review);
        }

        [Authorize]
        [Route("review")]
        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewRequestModel model)
        {
            var userId = _currentUserService.UserId;
            if (model.UserId == userId)
            {
                throw new ConflictException("Access Declind");
            }
            var review = await _reviewService.Update(model);
            return Ok(review);
        }

        [Authorize]
        [Route("{userId}/movie/{movieId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteReview(int userId, int movieId)
        {
            //var userID = _currentUserService.UserId;
            //if (userID == userId)
            //{
            //    throw new ConflictException("Access Declind");
            //}
            var review = await _reviewService.Delete(userId, movieId);
            return Ok(review);
        }

        [Authorize]
        [Route("{id}/purchase")]
        [HttpGet]
        public async Task<IActionResult> GetPurchase(int id)
        {
            //var userId = _currentUserService.UserId;
            var purchasedMovies = await _userService.GetPurchasedMovies(id);
            return Ok(purchasedMovies);
        }

        [Authorize]
        [Route("{id}/favorite")]
        [HttpGet]
        public async Task<IActionResult> GetFavorite(int id)
        {
            //var userId = _currentUserService.UserId;
            var favoriteMovies = await _userService.GetFavorites(id);
            return Ok(favoriteMovies);
        }

        [Authorize]
        [Route("{id}/reviews")]
        [HttpGet]
        public async Task<IActionResult> GetReviews (int id)
        {
            //var userId = _currentUserService.UserId;
            var movieReviews = await _userService.GetReviews(id);
            return Ok(movieReviews);
        }
    }
}
