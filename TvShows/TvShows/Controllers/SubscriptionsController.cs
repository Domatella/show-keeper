using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TvShows.Models;

namespace TvShows.Controllers
{
    public class SubscriptionsController : Controller
    {
        private ShowsContext db = new ShowsContext();

        private string getUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    return userIdClaim.Value;
                }
            }

            return "";
        }

        // GET: Subscriptions
        [Authorize()]
        public ActionResult Index()
        {
            string userId = getUserId();
            if (db.Purchases.Any(purchase => purchase.UserId == userId && purchase.IsActive == true))
            {
                var subscriptions = from s in db.UserSubscriptions
                                    join p in db.Purchases on s.PurchaseId equals p.PurchaseId
                                    where s.UserId == userId &&
                                    p.IsActive == true
                                    select s.SubscriptionId;
                ViewBag.Subscriptions = subscriptions.ToList();
            }
            else
            {
                ViewBag.Subscriptions = new List<int>();
            }

            return View(db.Subscriptions.ToList());
        }

        // GET: Subscriptions/Details/5
        [Authorize()]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // GET: Subscriptions/Create
        [Authorize()]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult Create([Bind(Include = "Id,Name,ImageUrl,Price")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Subscriptions.Add(subscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscription);
        }

        // GET: Subscriptions/Edit/5
        [Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult Edit([Bind(Include = "Id,Name,ImageUrl,Price")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscription);
        }

        // GET: Subscriptions/Delete/5
        [Authorize()]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            db.Subscriptions.Remove(subscription);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize()]
        private ActionResult basket()
        {
            var userId = getUserId();
            var currentPurchase = getCurrentPurchase(userId);

            if (currentPurchase == null)
            {
                return View(new List<Subscription>());
            }
            else
            {
                return View(getSubsInBucket(currentPurchase.PurchaseId, userId));
            }
        }

        [Authorize()]
        public ActionResult Basket(int subscriptionId, bool isDeleting = false)
        {
            if (subscriptionId == -1)
            {
                return basket();
            }

            var userId = getUserId();
            var currentPurchase = getCurrentPurchase(userId);

            if (!isDeleting)
            {
                if (currentPurchase == null)
                {
                    db.Purchases.Add(currentPurchase = new Purchase()
                    {
                        UserId = userId,
                        ItemsQuantity = 1,
                        IsActive = true
                    });
                    db.SaveChanges();
                }
                else
                {
                    currentPurchase.ItemsQuantity++;
                }

                db.UserSubscriptions.Add(new UserSubscription()
                {
                    PurchaseId = currentPurchase.PurchaseId,
                    SubscriptionId = subscriptionId,
                    UserId = userId
                });
                db.SaveChanges();
            }

            else
            {
                if (currentPurchase == null)
                {
                    return RedirectToAction("Index");
                }
                currentPurchase.ItemsQuantity--;
                if (currentPurchase.ItemsQuantity == 0)
                {
                    currentPurchase.IsActive = false;
                }
                var deletingItem = db.UserSubscriptions.Single(us => us.SubscriptionId == subscriptionId &&
                                                                     us.PurchaseId == currentPurchase.PurchaseId);

                db.UserSubscriptions.Remove(deletingItem);
                db.SaveChanges();
            }

            return View(getSubsInBucket(currentPurchase.PurchaseId, userId));
        }

        [Authorize()]
        public ActionResult Purchasing()
        {
            var currentPurchase = getCurrentPurchase(getUserId());
            currentPurchase.IsActive = false;
            db.SaveChanges();
            return View();
        }

        private Purchase getCurrentPurchase(string userId)
        {
            Purchase currentPurchase;

            try
            {
                if (db.Purchases.Count() != 0)
                {
                    currentPurchase = db.Purchases.First(p => p.IsActive == true && p.UserId == userId);
                }
                else
                {
                    currentPurchase = null;
                }
            }
            catch (InvalidOperationException)
            {
                currentPurchase = null;
            }

            return currentPurchase;
        }

        private List<Subscription> getSubsInBucket(int PurchaseId, string userId)
        {
            var subscriptionsInBucket = (from us in db.UserSubscriptions
                                         join subscription in db.Subscriptions on us.SubscriptionId equals subscription.Id
                                         where us.PurchaseId == PurchaseId &&
                                         us.UserId == userId
                                         select new
                                         {
                                             Id = subscription.Id,
                                             Name = subscription.Name,
                                             ImageUrl = subscription.ImageUrl,
                                             Price = subscription.Price
                                         }).AsEnumerable().Select(x => new Subscription()
                                         {
                                             Id = x.Id,
                                             Name = x.Name,
                                             ImageUrl = x.ImageUrl,
                                             Price = x.Price
                                         });
            return subscriptionsInBucket.ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
