using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;

namespace TvShows.BLL.Interfaces
{
    public interface ISubscriptionsService
    {
        SubscriptionDTO GetSubscription(int id);
        IEnumerable<SubscriptionDTO> GetAll();
        BasketDTO GetBasket(int userId);
        void Create(SubscriptionDTO subscription);
        PurchaseDTO GetCurrentPurchase(int userId);
        void AddPurchase(int userId);
        void UpdatePurchase(PurchaseDTO purchase);
        void PayPurchase(int purchaseId);
        void DeletePurchase(int purchaseId);
        void AddUserSubscription(int purchaseId, int subscriptionId);
        void DeleteUserSubscription(int purchaseId, int subscriptionId);
        void Update(SubscriptionDTO subscription);
        void Delete(int id);
        void Dispose();
    }
}
