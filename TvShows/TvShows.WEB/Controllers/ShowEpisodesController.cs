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
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserShowDTO, UserShow>();
                cfg.CreateMap<UserShowsViewDTO, UserShowsViewModel>()
                    .ForMember(x => x.UserShowsList,
                               x => x.MapFrom(list => Mapper.Map<IEnumerable<UserShowDTO>, 
                                                                 IEnumerable<UserShow>>(list.UserShowsList)));
            });
            var userShows = Mapper.Map<UserShowsViewDTO, UserShowsViewModel>(db.GetUsersShows(USER_ID));

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
            ViewBag.ShowId = showId;
            ViewBag.ShowName = showName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ShowId,Season,Episode")] ShowEpisodeViewModel showEpisode)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisodeViewModel, ShowEpisodeDTO>());
                db.Create(Mapper.Map<ShowEpisodeDTO>(showEpisode));
                return RedirectToAction("Index");
            }

            return View(showEpisode);
        }

        public ActionResult Edit(int? id, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisodeDTO, ShowEpisodeViewModel>());
            ShowEpisodeViewModel showEpisode = Mapper.Map<ShowEpisodeViewModel>(db.GetShowEpisode(id.Value));

            if (showEpisode == null)
            {
                return HttpNotFound();
            }

            ViewBag.Name = name;
            return View(showEpisode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShowEpisodeId,UserId,ShowId,Season,Episode")] ShowEpisodeViewModel showEpisode)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisodeViewModel, ShowEpisodeDTO>());
                db.Update(Mapper.Map<ShowEpisodeDTO>(showEpisode));
                return RedirectToAction("Index");
            }

            return View(showEpisode);
        }

        // GET: ShowEpisodes/Delete/5
        [Authorize()]
        public ActionResult Delete(int? id, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisodeDTO, ShowEpisodeViewModel>());
            ShowEpisodeViewModel showEpisode = Mapper.Map<ShowEpisodeViewModel>(db.GetShowEpisode(id.Value));
            if (showEpisode == null)
            {
                return HttpNotFound();
            }
            ViewBag.Name = name;
            return View(showEpisode);
        }

        // POST: ShowEpisodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Delete(id);
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