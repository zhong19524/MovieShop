using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Models;
namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        List<MovieCardResponseModel> GetTopRevenueMovies();
        
    }
}
