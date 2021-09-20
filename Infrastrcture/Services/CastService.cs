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
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }
        public async Task<CastResponseModel> GetCastDetailsById(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            var castmodel = new CastResponseModel()
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                ProfilePath = cast.ProfilePath,
                TmdbUrl = cast.TmdbUrl

            };

            castmodel.movieDetailResponseModels = new List<MovieDetailResponseModel>();

            foreach (var movie in cast.MovieCasts)
            {
                castmodel.movieDetailResponseModels.Add(new MovieDetailResponseModel {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    ReleaseDate = movie.Movie.ReleaseDate.Value,
                    Budget = movie.Movie.Budget,
                    PosterUrl = movie.Movie.PosterUrl,
                    Rating = movie.Movie.Rating
                });
            }
            return castmodel;
        }
    }
}
