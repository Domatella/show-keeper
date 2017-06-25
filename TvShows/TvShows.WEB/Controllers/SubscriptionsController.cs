using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TvShows.BLL.Interfaces;
using TvShows.WEB.Models;
using AutoMapper;
using TvShows.BLL.DTO;

namespace TvShows.WEB.Controllers
{
    public class SubscriptionsController : Controller
    {
        private const int USER_ID = 1;

        private ISubscriptionsService db { get; set; }

        public SubscriptionsController(ISubscriptionsService subscriptionsService)
        {
            db = subscriptionsService;
        }
        
        // GET: Subscriptions
        public ActionResult Index()
        {
            var dbSubscriptions = db.GetAll();
            var subscriptions = new List<SubscriptionViewModel>();

            foreach (var item in dbSubscriptions)
            {
                subscriptions.Add(new SubscriptionViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price
                });
            }

            return View(subscriptions);
        }

        public ActionResult Basket()
        {
            var dbBasket = db.GetBasket(USER_ID);
            if (dbBasket == null)
            {
                return View();
            }

            return View(createBasket(dbBasket));
        }

        public ActionResult AddToBasket(int subscriptionId)
        {
            var dbBasket = db.GetBasket(USER_ID);
            if (dbBasket == null)
            {
                db.AddPurchase(USER_ID);
                dbBasket = db.GetBasket(USER_ID);
            }

            db.AddUserSubscription(dbBasket.PurchaseId, subscriptionId);

            dbBasket = db.GetBasket(USER_ID);

            return View("Basket", createBasket(dbBasket));
        }

        public ActionResult DeleteFromBasket(int subscriptionId)
        {
            var dbBasket = db.GetBasket(USER_ID);
            db.DeleteUserSubscription(dbBasket.PurchaseId, subscriptionId);
            dbBasket = db.GetBasket(USER_ID);
            if (dbBasket.SubscriptionsList.Count == 0)
            {
                db.DeletePurchase(dbBasket.PurchaseId);
                return View("Basket");
            }

            return View("Basket", createBasket(dbBasket));
        }

        public ActionResult Purchasing()
        {
            db.PayPurchase(db.GetCurrentPurchase(USER_ID).Id);
            return View();
        }

        private BasketViewModel createBasket(BasketDTO dbBasket)
        {
            var basket = new BasketViewModel
            {
                PurchaseId = dbBasket.PurchaseId,
                UserId = dbBasket.UserId
            };

            foreach (var item in dbBasket.SubscriptionsList)
            {
                basket.SubscriptionsList.Add(new SubscriptionViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price
                });
            }

            return basket;
        }
    }
}