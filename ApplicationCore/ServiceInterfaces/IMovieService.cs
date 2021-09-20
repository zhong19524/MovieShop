using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();

        Task<List<MovieCardResponseModel>> GetTopRatedMovies();

        Task<List<ReviewResponseModel>> GetTopReviews(int id);
        Task<MovieDetailResponseModel> GetMovieDetails(int id);

        Task<List<MovieCardResponseModel>> GetMoviesByGenre(int id);

        Task<MovieCreateResponseModel> CreateMovie(MovieCreateRequestModel model);

        Task<MovieUpdateResponseModel> UpdateMovie(MovieUpdateRequestModel model);



    }
}
