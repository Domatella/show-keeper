using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TvShows.BLL.DTO;
using TvShows.WEB.Models;
using TvShows.BLL.Interfaces;
using System.Net;

namespace TvShows.WEB.Controllers
{
    public class ShowEpisodesController : Controller
    {
        private const int USER_ID = 1;

        private IShowEpisodesService db { get; set; }
        public ShowEpisodesController(IShowEpisodesService showsService)
        {
            db = showsService;
        }

        // GET: ShowEpisodes
        public ActionResult Index()
        {
            var dbUserShows = db.GetUsersShows(USER_ID);
            var userShows = new UserShowsViewModel() { UserId = dbUserShows.UserId };
            foreach (var show in dbUserShows.UserShowsList)
            {
                userShows.UserShowsList.Add(new UserShow
                {
                    ShowId = show.ShowId,
                    Name = show.Name,
                    Season = show.Season,
                    Episode = show.Episode,
                    ShowEpisodeId = show.ShowEpisodeId
                });
            }

            return View(userShows);
        }

        // GET: ShowEpisodes/Details/5
        public ActionResult Details(UserShow userShow)
        {
            if (userShow == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(userShow);
        }

        public ActionResult Create(int showId, string showName)
        {
            var dbUserShows = db.GetUsersShows(USER_ID);
            var userShows = new UserShowsViewModel() { UserId = dbUserShows.UserId };
            var showIds = new List<int>();

            foreach (var show in dbUserShows.UserShowsList)
            {
                userShows.UserShowsList.Add(new UserShow
                {
                    ShowId = show.ShowId,
                    Name = show.Name,
                    Season = show.Season,
                    Episode = show.Episode,
                    ShowEpisodeId = show.ShowEpisodeId
                });
                showIds.Add(show.ShowId);
            }

            if (showIds.Contains(showId))
            {
                return RedirectToAction("Edit", new
                {
                    id = userShows.UserShowsList.Single(us => us.ShowId == showId).ShowEpisodeId,
                    name = showName
                });
            }

            ViewBag.ShowId = showId;
            ViewBag.ShowName = showName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ShowId,Season,Episode")] ShowEpisodeViewModel showEpisode)
        {
            if (ModelState.IsValid && isQuantityValid(showEpisode))
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisodeViewModel, ShowEpisodeDTO>());
                db.Create(Mapper.Map<ShowEpisodeDTO>(showEpisode));
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Введены неверные значения";
            return View(showEpisode);
        }

        public ActionResult Edit(int? id, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dbShowEpisode = db.GetShowEpisode(id.Value);

            if (dbShowEpisode == null)
            {
                return HttpNotFound();
            }

            var showEpisode = new ShowEpisodeViewModel
            {
                Id = dbShowEpisode.Id,
                UserId = dbShowEpisode.UserId,
                ShowId = dbShowEpisode.ShowId,
                Episode = dbShowEpisode.Episode,
                Season = dbShowEpisode.Season
            };

            ViewBag.Name = name;
            return View(showEpisode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ShowId,Season,Episode")] ShowEpisodeViewModel showEpisode)
        {
            if (ModelState.IsValid && isQuantityValid(showEpisode))
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisodeViewModel, ShowEpisodeDTO>());
                db.Update(Mapper.Map<ShowEpisodeDTO>(showEpisode));
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = "Введены неверные значения";
            return View(showEpisode);
        }

        // GET: ShowEpisodes/Delete/5
        public ActionResult Delete(int? id, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dbShowEpisode = db.GetShowEpisode(id.Value);
            if (dbShowEpisode == null)
            {
                return HttpNotFound();
            }

            var showEpisode = new ShowEpisodeViewModel
            {
                Id = dbShowEpisode.Id,
                ShowId = dbShowEpisode.ShowId,
                UserId = dbShowEpisode.UserId,
                Season = dbShowEpisode.Season,
                Episode = dbShowEpisode.Episode
            };
            ViewBag.Name = name;

            return View(showEpisode);
        }

        // POST: ShowEpisodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

        private bool isQuantityValid(ShowEpisodeViewModel showEpisode)
        {
            var dbShow = db.GetShow(showEpisode.ShowId);
            if (showEpisode.Season < 1 || showEpisode.Season > dbShow.Seasons)
            {
                return false;
            }

            if (showEpisode.Episode < 1 || showEpisode.Episode > dbShow.Episodes)
            {
                return false;
            }

            return true;
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