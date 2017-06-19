using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TvShows.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace TvShows.Controllers
{
    public class ShowEpisodesController : Controller
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

        // GET: ShowEpisodes
        [Authorize()]
        public ActionResult Index()
        {
            string userId = getUserId();
            var shows = from episode in db.ShowEpisodes
                        join show in db.Shows on episode.ShowId equals show.ShowId
                        where episode.UserId == userId  
                        select new UserShowsView() { ShowEpisodeId = episode.ShowEpisodeId,
                                                     ShowId = show.ShowId,
                                                     Name = show.Name,
                                                     Season = episode.Season,
                                                     Episode = episode.Episode };
            return View(shows.ToList());
        }

        // GET: ShowEpisodes/Details/5
        [Authorize()]
        public ActionResult Details(UserShowsView usv)
        {
            if (usv == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(usv);
        }

        // GET: ShowEpisodes/Create
        [Authorize()]
        public ActionResult Create(int showId, string showName)
        {
            ViewBag.ShowId = showId;
            ViewBag.ShowName = showName;
            ViewBag.UserId = getUserId();
            return View();
        }

        // POST: ShowEpisodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult Create([Bind(Include = "ShowEpisodeId,UserId,ShowId,Season,Episode")] ShowEpisode showEpisode)
        {
            if (ModelState.IsValid)
            {
                db.ShowEpisodes.Add(showEpisode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(showEpisode);
        }

        // GET: ShowEpisodes/Edit/5
        [Authorize()]
        public ActionResult Edit(int? id, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowEpisode showEpisode = db.ShowEpisodes.Find(id);
            if (showEpisode == null)
            {
                return HttpNotFound();
            }

            ViewBag.Name = name;
            return View(showEpisode);
        }

        // POST: ShowEpisodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult Edit([Bind(Include = "ShowEpisodeId,UserId,ShowId,Season,Episode")] ShowEpisode showEpisode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(showEpisode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(showEpisode);
        }

        // GET: ShowEpisodes/Delete/5
        [Authorize()]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowEpisode showEpisode = db.ShowEpisodes.Find(id);
            if (showEpisode == null)
            {
                return HttpNotFound();
            }
            return View(showEpisode);
        }

        // POST: ShowEpisodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult DeleteConfirmed(int id)
        {
            ShowEpisode showEpisode = db.ShowEpisodes.Find(id);
            db.ShowEpisodes.Remove(showEpisode);
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
