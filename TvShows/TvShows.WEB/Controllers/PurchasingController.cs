using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TvShows.BLL.DTO;
using TvShows.BLL.Interfaces;
using TvShows.WEB.Models;

namespace TvShows.WEB.Controllers
{
    public class PurchasingController : Controller
    {
        private const int USER_ID = 1;

        private ISubscriptionsService db { get; set; }

        public PurchasingController(ISubscriptionsService subscriptionsService)
        {
            db = subscriptionsService;
        }

        // GET: Purchasing
        public ActionResult Index()
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

            return RedirectToAction("Index");
        }

        public ActionResult DeleteFromBasket(int subscriptionId)
        {
            var dbBasket = db.GetBasket(USER_ID);
            db.DeleteUserSubscription(dbBasket.PurchaseId, subscriptionId);
            dbBasket = db.GetBasket(USER_ID);
            if (dbBasket.SubscriptionsList.Count == 0)
            {
                db.DeletePurchase(dbBasket.PurchaseId);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
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