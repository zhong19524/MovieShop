//using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel model);

        Task<UserLoginResponseModel> Login(LoginRequestModel model);

        Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMovies(int userId);

        Task<IEnumerable<MovieCardResponseModel>> GetFavorites(int userId);

        Task<IEnumerable<ReviewResponseModel>> GetReviews(int userId);

        
        //Task<FavoriteRequestModel> FavoriteMovie(int userId, int movieId);
        //Task<ReviewRequestModel> ReviewMovie(int userId, int movieId);
        

        Task<UserProfileResponseModel> GetProfile(int userId);

    }
}
