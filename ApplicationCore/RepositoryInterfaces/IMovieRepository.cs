using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository:IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> Get30HighestRevenueMovies();

        Task<IEnumerable<Movie>> Get30TopRatedMovies();

        Task<Genre> GetMoviesByGenre(int id);

        Task<IEnumerable<Review>> GetTop30Reviews(int id);

       
    }
}
