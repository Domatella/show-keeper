using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TvShows.BLL.Interfaces;
using TvShows.WEB.Models;
using AutoMapper;
using TvShows.BLL.DTO;
using System.Net;

namespace TvShows.WEB.Controllers
{
    public class ShowController : Controller
    {
        private IShowsService db { get; set; }
        public ShowController(IShowsService showsService)
        {
            db = showsService;
        }

        public ActionResult Index(string searchString, int page = 1)
        {
            int pageSize = 5;

            var shows = new List<ShowViewModel>();
            var dbShows = db.GetShows();
            foreach(var show in dbShows)
            {
                shows.Add(new ShowViewModel
                {
                    Id = show.Id,
                    Name = show.Name,
                    Seasons = show.Seasons,
                    Episodes = show.Episodes,
                    Description = show.Description
                });
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                shows = shows.Where(show => show.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            IEnumerable<ShowViewModel> showsPerPage = shows.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = shows.Count() };
            PageIndexViewModel ivm = new PageIndexViewModel { PageInfo = pageInfo, Shows = showsPerPage };

            return View(ivm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dbShow = db.GetShow(id.Value);
            if (dbShow == null)
            {
                return HttpNotFound();
            }

            var show = new ShowViewModel
            {
                Id = dbShow.Id,
                Name = dbShow.Name,
                Seasons = dbShow.Seasons,
                Episodes = dbShow.Episodes,
                Description = dbShow.Description
            };
            return View(show);
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