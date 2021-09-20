using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);

        Task<IEnumerable<Purchase>> GetPurchasedById(int id);
        Task<IEnumerable<Favorite>> GetFavoriteById(int id);

        //Task<Favo>
        Task<IEnumerable<Review>> GetReviewsById(int id);
        Task<User> GetProfileById(int Id);
    }
}
