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
    public class ShowsController : Controller
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

        // GET: Shows
        [Authorize()]
        public ActionResult Index(string searchString, int page = 1)
        {
            string userId = getUserId();
            var userShows = from showEp in db.ShowEpisodes
                             where showEp.UserId == userId
                             select showEp.ShowId;
            ViewBag.AddedShows = userShows.ToList();

            int pageSize = 10;
            var shows = db.Shows.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                shows = shows.Where(show => show.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            IEnumerable<Show> showsPerPage = shows.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.Shows.Count() };
            PageIndexViewModel ivm = new PageIndexViewModel { PageInfo = pageInfo, Shows = showsPerPage };

            return View(ivm);
        }

        // GET: Shows/Details/5
        [Authorize()]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        // GET: Shows/Create
        [Authorize()]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult Create([Bind(Include = "ShowId,Name,Seasons,Series,Description")] Show show)
        {
            if (ModelState.IsValid)
            {
                db.Shows.Add(show);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(show);
        }

        // GET: Shows/Edit/5
        [Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult Edit([Bind(Include = "ShowId,Name,Seasons,Series,Description")] Show show)
        {
            if (ModelState.IsValid)
            {
                db.Entry(show).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(show);
        }

        // GET: Shows/Delete/5
        [Authorize()]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult DeleteConfirmed(int id)
        {
            Show show = db.Shows.Find(id);
            db.Shows.Remove(show);
            db.SaveChanges();
            return RedirectToAction("Index");
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
