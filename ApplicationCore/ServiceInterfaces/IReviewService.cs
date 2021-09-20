using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IReviewService
    {
        Task<ReviewRequestModel> Review(ReviewRequestModel model);

        Task<ReviewRequestModel> Update(ReviewRequestModel model);

        Task<ReviewRequestModel> Delete(int userid,int movieid);
    }
}
