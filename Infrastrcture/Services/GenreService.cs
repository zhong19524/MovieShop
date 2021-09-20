using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
namespace Infrastrcture.Services
{
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _genreRepository;

        private readonly IMemoryCache _memoryCache;

        public GenreService(IAsyncRepository<Genre> genreRepository, IMemoryCache memoryCache)
        {
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
        }
        public async Task<IEnumerable<GenreResponseModel>> GetAllGenres()
        {
            // check if the cache has the genres, if yes then take genres from cache
            // If NO, then go to database and get the genres and store in cache
            // .NET In Memoery Caching => smaller amount of data
            // Distributed caching -> Redis Cache => Large amount of data
            var genres = await _memoryCache.GetOrCreate("genreCache", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(30);
                return await _genreRepository.ListAllAsync();
            });

            var genremodel = new List<GenreResponseModel>();
            foreach (var genre in genres)
            {
                genremodel.Add(new GenreResponseModel { Id = genre.Id, Name = genre.Name });
            }
            return genremodel;
        }

        
    }
}
