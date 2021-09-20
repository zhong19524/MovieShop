using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrcture.Services
{
    public class FavoriteService: IFavoriteService
    {
        private readonly IAsyncRepository<Favorite> _favoriteRepository;

        public FavoriteService(IAsyncRepository<Favorite> favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<FavoriteRequestModel> Favorite(FavoriteRequestModel model)
        {
            var favorite = new Favorite
            {
                UserId = model.UserId,
                MovieId = model.MovieId
            };
            var createFavorite = await _favoriteRepository.AddAsync(favorite);

            var response = new FavoriteRequestModel
            {
                MovieId  = createFavorite.MovieId,
                UserId =    createFavorite.UserId
            };
            return response;
        }
        public async Task<FavoriteRequestModel> UnFavorite(FavoriteRequestModel model)
        {
            var favorite = new Favorite
            {
                UserId = model.UserId,
                MovieId = model.MovieId
            };
            var unfavorite = await _favoriteRepository.DeleteAsync(favorite);

            var response = new FavoriteRequestModel
            {
                MovieId = unfavorite.MovieId,
                UserId = unfavorite.UserId
            };
            return response;
        }

    }
}
