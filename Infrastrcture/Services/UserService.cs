using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
namespace Infrastrcture.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

 
        public async Task<UserProfileResponseModel> GetProfile(int userId)
        {
            var dbUser = await _userRepository.GetProfileById(userId);
            var userprofile = new UserProfileResponseModel
            {
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Email = dbUser.Email,
                DateOfBirth = dbUser.DateOfBirth
            };
            return userprofile;

        }


        public async Task<IEnumerable<MovieCardResponseModel>> GetFavorites(int userId)
        {
            var favorites = await _userRepository.GetFavoriteById(userId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var favorite in favorites)
            {
                movieCards.Add(new MovieCardResponseModel { Id = favorite.Movie.Id, Title = favorite.Movie.Title, PosterUrl = favorite.Movie.PosterUrl });
            }
            return movieCards;
        }



        public async Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMovies(int userId)
        {
            var purchases = await _userRepository.GetPurchasedById(userId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var purchase in purchases)
            {
                movieCards.Add(new MovieCardResponseModel { Id = purchase.Movie.Id, Title = purchase.Movie.Title, PosterUrl = purchase.Movie.PosterUrl });
            }
            return movieCards;
        }

        public async Task<IEnumerable<ReviewResponseModel>> GetReviews(int userId)
        {
            var reviews = await _userRepository.GetReviewsById(userId);
            var reviewModels = new List<ReviewResponseModel>();
            foreach (var review in reviews)
            {
                reviewModels.Add(new ReviewResponseModel { Id = review.UserId, FirstName = review.User.FirstName, LastName = review.User.LastName, Rating = review.Rating, ReviewText = review.ReviewText, Title = review.Movie.Title, PosterUrl = review.Movie.PosterUrl });
            }
            return reviewModels;
        }

       

        public async Task<UserLoginResponseModel> Login(LoginRequestModel model)
        {

            var dbUser = await _userRepository.GetUserByEmail(model.Email);

            if (dbUser == null)
            {
                return null;
            }
            var hashedpassword = GetHashedPassword(model.PassWord, dbUser.Salt);

            if (hashedpassword == dbUser.HashedPassword)
            {
                //success login
                var userLoginResponsModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName
                };
                return userLoginResponsModel;
            }

            else
            {
                return null;
            }

        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            // user already has email // email already exist
            if (dbUser != null)
            {
                throw new ConflictException("Email already exist");
            }

            // user does not exists in the database
            // generate a uniqueue salt / hashedpassword
            var salt = GenerateSalt();
            var hashedpassword = GetHashedPassword(model.Password, salt);

            // save the salt and hashed password to the database.
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salt = salt,
                HashedPassword = hashedpassword
            };

            var createduser = await _userRepository.AddAsync(user);

            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createduser.Id,
                Email = createduser.Email,
                FirstName = createduser.FirstName,
                LastName = createduser.LastName,
            };
            return userRegisterResponseModel;


        }

    
        //Unique slat generation
        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password,string salt)
        {
            // Never ever create your own HAshing Algorithms
            // always use Industry tried and tested HAshing Algorithms
            // Aarogon2 
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                     password: password,
                                                                     salt: Convert.FromBase64String(salt),
                                                                     prf: KeyDerivationPrf.HMACSHA512,
                                                                     iterationCount: 10000,
                                                                     numBytesRequested: 256 / 8));
            return hashed;
        }

        
    }



}
