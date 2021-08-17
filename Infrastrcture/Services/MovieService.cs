using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;

namespace Infrastrcture.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            var movieDetailsModel = new MovieDetailResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                PosterUrl = movie.PosterUrl,
                RunTime = movie.RunTime,
                ReleaseDate = movie.ReleaseDate,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                Overview =movie.Overview
                
                
            };

            movieDetailsModel.Casts = new List<CastResponseModel>();
            foreach (var cast in movie.MovieCasts)
            {
                movieDetailsModel.Casts.Add(new CastResponseModel { Id = cast.CastId, Name = cast.Cast.Name, character = cast.Character, ProfilePath= cast.Cast.ProfilePath });
            }

            movieDetailsModel.Genres = new List<GenreResponseModel>();
            foreach (var genre in movie.Genres)
            {
                movieDetailsModel.Genres.Add(new GenreResponseModel { Id = genre.Id, Name = genre.Name });
            }

            return movieDetailsModel;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.Get30HighestRevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }

            return movieCards;
        }
    }
}
