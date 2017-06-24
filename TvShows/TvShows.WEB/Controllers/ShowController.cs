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
        private IShowsEpisodeService db { get; set; }
        public ShowController(IShowsEpisodeService showsService)
        {
            db = showsService;
        }

        public ActionResult Index(string searchString, int page = 1)
        {
            int pageSize = 5;

            Mapper.Initialize(cfg => cfg.CreateMap<ShowDTO, ShowViewModel>().ReverseMap());
            var shows = Mapper.Map<IEnumerable<ShowDTO>, IEnumerable<ShowViewModel>>(db.GetShows());
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

            Mapper.Initialize(cfg => cfg.CreateMap<ShowDTO, ShowViewModel>());
            var show = Mapper.Map<ShowDTO, ShowViewModel>(db.GetShow(id.Value));
            if (show == null)
            {
                return HttpNotFound();
            }
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