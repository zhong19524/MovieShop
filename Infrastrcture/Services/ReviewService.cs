using ApplicationCore.Entities;
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
    public class ReviewService:IReviewService
    {
        private readonly IAsyncRepository<Review> _reviewRepository;

        public ReviewService(IAsyncRepository<Review> reviwRepository)
        {
            _reviewRepository = reviwRepository;
        }

        public async Task<ReviewRequestModel> Delete(int userId,int movieId)
        {
            var reviews = await _reviewRepository.ListAsync(r=>(r.UserId ==userId & r.MovieId ==movieId));
            //var deletereviews = new List<ReviewResponseModel>();
            foreach (var review in reviews)
            {
                var deleteReview = await _reviewRepository.DeleteAsync(review);
                //deletereviews.Add(new ReviewResponseModel {Id=review.UserId,Rating=review.Rating,ReviewText=review.ReviewText });
            }
            //var deleteReview = await _reviewRepository.DeleteAsync(review);
            var response = new ReviewRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };
            return response;
        }

        public async Task<ReviewRequestModel> Review(ReviewRequestModel model)
        {
            var review = new Review
            {
                UserId =model.UserId,
                MovieId = model.MovieId,
                Rating = model.Rating,
                ReviewText = model.ReviewText
            };
            var createReview = await _reviewRepository.AddAsync(review);

            var response = new ReviewRequestModel
            {
                MovieId = createReview.MovieId,
                UserId = createReview.UserId
            };
            return response;
        }

        public async Task<ReviewRequestModel> Update(ReviewRequestModel model)
        {
            var review = new Review
            {
                UserId = model.UserId,
                MovieId = model.MovieId,
                Rating = model.Rating,
                ReviewText = model.ReviewText
            };
            var createReview = await _reviewRepository.UpdateAsync(review);

            var response = new ReviewRequestModel
            {
                MovieId = createReview.MovieId,
                UserId = createReview.UserId
            };
            return response;
        }

        

    }
}
