using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        private readonly IMovieService _movieService;
        private readonly IPurchaseService _purchaseService;
        public AdminController(ICurrentUserService currentUserService, IMovieService movieService, IPurchaseService purchaseService)
        {
            _currentUserService = currentUserService;
            _movieService = movieService;
            _purchaseService = purchaseService;
        }
        [Route("movie")]
        [HttpPost]
        public async Task<IActionResult> PostMovie([FromBody] MovieCreateRequestModel model)
        {
            //var userId = _currentUserService.UserId;
            //var isAdmin = _currentUserService.IsAdmin;
            //if (!isAdmin)
            //{
            //    throw new ConflictException("Access Declind (Not Admin)");
            //}

            var createmovie = await _movieService.CreateMovie(model);
            return Ok(createmovie);
        }


        [Route("movie")]
        [HttpPut]
        public async Task<IActionResult> UpdateMovie([FromBody] MovieUpdateRequestModel model)
        {
            //var userId = _currentUserService.UserId;
            //var isAdmin = _currentUserService.IsAdmin;
            //if (!isAdmin)
            //{
            //    throw new ConflictException("Access Declind (Not Admin)");
            //}

            var updatemovie = await _movieService.UpdateMovie(model);
            return Ok(updatemovie);
        }

        [Route("purchase")]
        [HttpGet]
        public async Task<IActionResult> GetPurchase()
        {
            var purchases = await _purchaseService.GetAllPurchase();
            return Ok(purchases);
        }

    }
}
