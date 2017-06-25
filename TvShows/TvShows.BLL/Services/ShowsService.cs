using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;
using TvShows.BLL.Interfaces;
using TvShows.DAL.Interfaces;
using TvShows.DAL.Entities;

namespace TvShows.BLL.Services
{
    public class ShowsService : IShowsService
    {
        private IUnitOfWork db { get; set; }

        public ShowsService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public void Create(ShowDTO show)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ShowDTO, Show>());
            db.Shows.Create(Mapper.Map<Show>(show));
            db.Save();
        }

        public void Delete(int id)
        {
            db.Shows.Delete(id);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public ShowDTO GetShow(int id)
        {
            var show = db.Shows.Get(id);

            if (show == null)
            {
                throw new ArgumentException();
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Show, ShowDTO>());
            return Mapper.Map<ShowDTO>(show);
        }

        public IEnumerable<ShowDTO> GetShows()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Show, ShowDTO>().ReverseMap();
            });
            var shows = Mapper.Map<IEnumerable<Show>, IEnumerable<ShowDTO>>(db.Shows.GetAll());
            return shows;
        }

        public void Update(ShowDTO show)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ShowDTO, Show>());
            db.Shows.Update(Mapper.Map<Show>(show));
        }
    }
}
