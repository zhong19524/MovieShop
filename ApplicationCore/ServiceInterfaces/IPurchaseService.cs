using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IPurchaseService
    {
        Task<PurchaseResponseModel> Purchase(PurchaseRequestModel model);
        Task<IEnumerable<PurchaseResponseModel>> GetAllPurchase();
    }
}
