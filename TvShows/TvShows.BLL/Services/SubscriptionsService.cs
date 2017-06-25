using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;
using TvShows.BLL.Interfaces;
using TvShows.DAL.Entities;
using TvShows.DAL.Interfaces;
using TvShows.DAL.Repositories;

namespace TvShows.BLL.Services
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private IUnitOfWork db { get; set; }

        public SubscriptionsService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public void Create(SubscriptionDTO subscription)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SubscriptionDTO, Subscription>());
            db.Subscriptions.Create(Mapper.Map<Subscription>(subscription));
            db.Save();
        }

        public void Delete(int id)
        {
            db.Subscriptions.Delete(id);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public BasketDTO GetBasket(int userId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Purchase, PurchaseDTO>());
            var purchase = Mapper.Map<PurchaseDTO>(db.Purchases.Get(userId));

            if (purchase == null)
            {
                return null;
            }

            Mapper.Initialize(cfg => cfg.CreateMap<UserSubscription, UserSubscriptionDTO>());
            var items = Mapper.Map<IEnumerable<UserSubscriptionDTO>>(db.UserSubscriptions.GetAll()
                .Where(us => us.PurchaseId == purchase.Id));

            var subscriptionsIds = new List<int>();
            foreach (var item in items)
            {
                subscriptionsIds.Add(item.SubscriptionId);
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Subscription, SubscriptionDTO>());
            var subscriptions = Mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionDTO>>(db.Subscriptions.GetAll()
                .Where(s => subscriptionsIds.Contains(s.Id)));

            var basket = new BasketDTO() { UserId = userId, PurchaseId = purchase.Id };
            basket.SubscriptionsList.AddRange(subscriptions);

            return basket;
        }

        public SubscriptionDTO GetSubscription(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Subscription, SubscriptionDTO>());
            return Mapper.Map<SubscriptionDTO>(db.Subscriptions.Get(id));
        }

        public void Update(SubscriptionDTO subscription)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SubscriptionDTO, Subscription>());
            db.Subscriptions.Update(Mapper.Map<Subscription>(subscription));
            db.Save();
        }

        public IEnumerable<SubscriptionDTO> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Subscription, SubscriptionDTO>());
            return Mapper.Map<IEnumerable<Subscription>, IEnumerable<SubscriptionDTO>>(db.Subscriptions.GetAll());
        }

        public void AddUserSubscription(int purchaseId, int subscriptionId)
        {
            db.UserSubscriptions.Create(new UserSubscription
            {
                PurchaseId = purchaseId,
                SubscriptionId = subscriptionId
            });
            db.Save();
        }

        public void AddPurchase(int userId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PurchaseDTO, Purchase>());
            db.Purchases.Create(Mapper.Map<Purchase>(new PurchaseDTO
            {
                UserId = userId,
                IsPaid = false
            }));
            db.Save();
        }

        public void DeletePurchase(int purchaseId)
        {
            db.Purchases.Delete(purchaseId);
            db.Save();
        }

        public void DeleteUserSubscription(int purchaseId, int subscriptionId)
        {
            var deleting = db.UserSubscriptions.Find(us => 
                (us.PurchaseId == purchaseId) && (us.SubscriptionId == subscriptionId));
            db.UserSubscriptions.Delete(deleting.First().Id);
            db.Save();
        }

        public void UpdatePurchase(PurchaseDTO purchase)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PurchaseDTO, Purchase>());
            db.Purchases.Update(Mapper.Map<Purchase>(purchase));
            db.Save();
        }

        public PurchaseDTO GetCurrentPurchase(int userId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Purchase, PurchaseDTO>());
            return Mapper.Map<PurchaseDTO>(db.Purchases.Get(userId));
        }

        public void PayPurchase(int purchaseId)
        {
            var purchase = db.Purchases.Find(p => p.Id == purchaseId);
            if (purchase.Count() == 1)
            {
                purchase.First().IsPaid = true;
                db.Purchases.Update(purchase.First());
                db.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
