using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastrcture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrcture.Repository
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }
        public async Task<IEnumerable<Movie>> Get30HighestRevenueMovies()
        {
            //SELECT top 30 from movies order by revenue
            //ToList(), Count() or we can loop through
            // I/O bound operation
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> Get30TopRatedMovies()
        {
            var movies = await _dbContext.Movies.ToListAsync();
            foreach (var movie in movies)
            {
                movie.Rating = await _dbContext.Reviews.Where(m => m.MovieId == movie.Id).DefaultIfEmpty().AverageAsync(r => r == null ? 0 : r.Rating);
            }
            movies.OrderByDescending(m => m.Rating).Take(30);
            //var moviesRateing = await _dbContext.Reviews.DefaultIfEmpty().AverageAsync(r => r == null ? 0 : r.Rating);
            //var movies = await _dbContext.Movies.Include(m=>m.Reviews).GroupBy(r => r.movie).ToListAsync();
            
            //foreach (var movie in movies)
            //{
            //    movie.Rating = 
            //}
            return movies;
        }

        public async Task<IEnumerable<Review>> GetTop30Reviews(int id)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.User).Where(r=> r.MovieId == id).OrderByDescending(r => r.Rating).Take(30).ToListAsync();
            return reviews;
        }


        public override async Task<Movie> GetByIdAsync(int id)
        {
            // movie table, then genres, then casts and rating
            // Include() ThenInclude()

            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                throw new Exception($"No Movie Found for the id {id}");
            }

            var movieRating = await _dbContext.Reviews.Where(m => m.MovieId == id).DefaultIfEmpty().AverageAsync(r => r == null ? 0 : r.Rating);

            movie.Rating = movieRating;
            return movie;

        }

        public async Task<Genre> GetMoviesByGenre(int id)
        {
            var genreMovies = await _dbContext.Genres.Include(g=>g.Movies).FirstOrDefaultAsync(g =>g.Id == id);
            if (genreMovies == null)
            {
                throw new Exception($"No Movie Found for the genre {id}");
            }
            return genreMovies;
        }

        
    }
}
