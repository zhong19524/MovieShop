using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System.Threading.Tasks;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;

namespace Infrastrcture.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        public async Task<MovieCreateResponseModel> CreateMovie(MovieCreateRequestModel model)
        {
            var movie = new Movie
            {
                Id = model.Id,
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Revenue = model.Revenue,
                Budget = model.Budget,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                Genres = model.Genres,
                CreatedDate = model.CreatedDate,
                CreatedBy = model.CreatedBy

            };

            var createMovie = await _movieRepository.AddAsync(movie);

            var movieModel = new MovieCreateResponseModel
            {
                Id = createMovie.Id,
                Title = createMovie.Title,
                Overview = createMovie.Overview,
                Tagline = createMovie.Tagline,
                Revenue = createMovie.Revenue,
                Budget = createMovie.Budget,
                ImdbUrl = createMovie.ImdbUrl,
                TmdbUrl = createMovie.TmdbUrl,
                PosterUrl = createMovie.PosterUrl,
                BackdropUrl = createMovie.BackdropUrl,
                OriginalLanguage = createMovie.OriginalLanguage,
                ReleaseDate = createMovie.ReleaseDate,
                RunTime = createMovie.RunTime,
                Price = createMovie.Price,
                Genres = (List<Genre>)createMovie.Genres,
                CreatedDate = createMovie.CreatedDate,
                CreatedBy = createMovie.CreatedBy
            };

            return movieModel;

        }

        public async Task<MovieUpdateResponseModel> UpdateMovie(MovieUpdateRequestModel model)
        {
            var movie = new Movie
            {
                Id = model.Id,
                Title = model.Title,
                Overview = model.Overview,
                Tagline = model.Tagline,
                Revenue = model.Revenue,
                Budget = model.Budget,
                ImdbUrl = model.ImdbUrl,
                TmdbUrl = model.TmdbUrl,
                PosterUrl = model.PosterUrl,
                BackdropUrl = model.BackdropUrl,
                OriginalLanguage = model.OriginalLanguage,
                ReleaseDate = model.ReleaseDate,
                RunTime = model.RunTime,
                Price = model.Price,
                Genres = model.Genres,
                UpdatedDate = model.UpdatedDate,
                UpdatedBy = model.UpdatedBy

            };

            var updateMovie = await _movieRepository.UpdateAsync(movie);

            var movieModel = new MovieUpdateResponseModel
            {
                Id = updateMovie.Id,
                Title = updateMovie.Title,
                Overview = updateMovie.Overview,
                Tagline = updateMovie.Tagline,
                Revenue = updateMovie.Revenue,
                Budget = updateMovie.Budget,
                ImdbUrl = updateMovie.ImdbUrl,
                TmdbUrl = updateMovie.TmdbUrl,
                PosterUrl = updateMovie.PosterUrl,
                BackdropUrl = updateMovie.BackdropUrl,
                OriginalLanguage = updateMovie.OriginalLanguage,
                ReleaseDate = updateMovie.ReleaseDate,
                RunTime = updateMovie.RunTime,
                Price = updateMovie.Price,
                Genres = (List<Genre>)updateMovie.Genres,
                UpdatedBy = updateMovie.UpdatedBy,
                UpdatedDate = updateMovie.UpdatedDate
            };

            return movieModel;
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

        public async Task<List<MovieCardResponseModel>> GetMoviesByGenre(int id)
        {
            var genreMovies = await _movieRepository.GetMoviesByGenre(id);
            //var movies = genreMovies.Movies;
            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in genreMovies.Movies)
            {
                movieCards.Add(new MovieCardResponseModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }

            return movieCards;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.Get30TopRatedMovies();
            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }

            return movieCards;
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

        public async Task<List<ReviewResponseModel>> GetTopReviews(int id)
        {
            var reviews = await _movieRepository.GetTop30Reviews(id);
            var reviewModel = new List<ReviewResponseModel>();

            foreach (var review in reviews)
            {
                reviewModel.Add(new ReviewResponseModel { Id = review.UserId, FirstName = review.User.FirstName, LastName = review.User.LastName, Rating = review.Rating, ReviewText = review.ReviewText, 
                    Title = review.Movie?.Title, PosterUrl = review.Movie?.PosterUrl });
                //reviewModel.Add(new ReviewRequestModel
                //{
                //    Id = review.UserId,
                //    FirstName = review.User.FirstName,
                //    LastName = review.User.LastName,
                //    Rating = review.Rating,
                //    ReviewText = review.ReviewText
                //});
            }
            return reviewModel;
        }

        
    }
}
