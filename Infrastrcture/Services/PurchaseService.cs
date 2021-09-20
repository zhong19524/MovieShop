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
    public class PurchaseService : IPurchaseService
    {
        private readonly IAsyncRepository<Purchase> _purchaseRepository;

        public PurchaseService(IAsyncRepository<Purchase> purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IEnumerable<PurchaseResponseModel>> GetAllPurchase()
        {
            var purchases = await _purchaseRepository.ListAllAsync();
            var purchasesModel = new List<PurchaseResponseModel>();

            foreach (var purchase in purchases)
            {
                purchasesModel.Add(new PurchaseResponseModel { 
                    UserId = purchase.UserId, MovieId = purchase.MovieId, TotalPrice = purchase.TotalPrice,PurchaseDateTime = purchase.PurchaseDateTime,PurchaseNumber=purchase.PurchaseNumber 
                });
            }
            return purchasesModel;
        }

        public async Task<PurchaseResponseModel> Purchase(PurchaseRequestModel model)
        {
            var purchase = new Purchase
            {
                UserId = model.UserId,
                MovieId = model.MovieId,
                PurchaseDateTime = model.PurchaseDateTime,
                TotalPrice = model.TotalPrice,
                PurchaseNumber = model.PurchaseNumber
            };
            var createPurchase = await _purchaseRepository.AddAsync(purchase);

            var response = new PurchaseResponseModel
            {
                UserId = createPurchase.UserId,
                MovieId = createPurchase.MovieId,
                PurchaseDateTime = createPurchase.PurchaseDateTime,
                TotalPrice = createPurchase.TotalPrice,
                PurchaseNumber = createPurchase.PurchaseNumber

            };
            return response;
        }


    }
}
