using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;
using TvShows.BLL.Interfaces;
using TvShows.DAL.Interfaces;
using TvShows.DAL.Entities;
using AutoMapper;

namespace TvShows.BLL.Services
{
    public class ShowEpisodesService : IShowEpisodesService
    {
        private IUnitOfWork db { get; set; }

        public ShowEpisodesService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public void Create(ShowEpisodeDTO showEpisode)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisodeDTO, ShowEpisode>());
            db.ShowEpisodes.Create(Mapper.Map<ShowEpisode>(showEpisode));
            db.Save();
        }

        public void Delete(int id)
        {
            db.ShowEpisodes.Delete(id);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public ShowEpisodeDTO GetShowEpisode(int id)
        {
            var showEpisode = db.ShowEpisodes.Get(id);

            if (showEpisode == null)
            {
                throw new ArgumentException();
            }

            Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisode, ShowEpisodeDTO>());
            return Mapper.Map<ShowEpisodeDTO>(showEpisode);
        }

        public UserShowsViewDTO GetUsersShows(int userId)
        {
            var usersShowView = new UserShowsViewDTO();
            Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisode, ShowEpisodeDTO>());
            var showsEpisodes = Mapper.Map<IEnumerable<ShowEpisode>, IEnumerable<ShowEpisodeDTO>>(db.ShowEpisodes.GetAll()
                .Where(se => se.UserId == userId));

            Mapper.Initialize(cfg => cfg.CreateMap<Show, ShowDTO>());

            var shows = from showEpisode in showsEpisodes
                        join show in db.Shows.GetAll() on showEpisode.ShowId equals show.Id
                        select new { Id = show.Id, Name = show.Name };

            foreach (var item in showsEpisodes)
            {
                usersShowView.UserShowsList.Add(new UserShowDTO()
                {
                    ShowEpisodeId = item.Id,
                    ShowId = item.ShowId,
                    Name = shows.Single(show => show.Id == item.ShowId).Name,
                    Season = item.Season,
                    Episode = item.Episode
                });
            }

            return usersShowView;
        }

        public void Update(ShowEpisodeDTO showEpisode)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ShowEpisodeDTO, ShowEpisode>());
            db.ShowEpisodes.Update(Mapper.Map<ShowEpisode>(showEpisode));
            db.Save();
        }

        public ShowDTO GetShow(int showId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Show, ShowDTO>());
            return Mapper.Map<ShowDTO>(db.Shows.Get(showId));
        }
    }
}
