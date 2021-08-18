using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastrcture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastrcture.Repository
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Favorite>> GetFavoriteById(int id)
        {
            var favorites = await _dbContext.Favorites.Include(f => f.Movie).Where(f => f.UserId == id).ToListAsync();
            return favorites;
        }

        public async Task<User> GetProfileById(int id)
        {
            var profile = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return profile;
        }

        public async Task<IEnumerable<Purchase>> GetPurchasedById(int id)
        {
            var purchases =  await _dbContext.Purchases.Include(p=>p.Movie).Where(p => p.UserId == id).ToListAsync();

            return purchases;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
