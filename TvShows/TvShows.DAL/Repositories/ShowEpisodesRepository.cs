using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.DAL.EF;
using TvShows.DAL.Entities;
using TvShows.DAL.Interfaces;

namespace TvShows.DAL.Repositories
{
    class ShowEpisodesRepository : IRepository<ShowEpisode>
    {
        private KeeperContext db;

        public ShowEpisodesRepository(KeeperContext context)
        {
            db = context;
        }

        public void Create(ShowEpisode item)
        {
            db.ShowEpisodes.Add(item);
        }

        public void Delete(int id)
        {
            ShowEpisode showEpisode = db.ShowEpisodes.Find(id);
            if (showEpisode != null)
            {
                db.ShowEpisodes.Remove(showEpisode);
            }
        }

        public IEnumerable<ShowEpisode> Find(Func<ShowEpisode, bool> predicate)
        {
            return db.ShowEpisodes.Where(predicate).ToList();
        }

        public ShowEpisode Get(int id)
        {
            return db.ShowEpisodes.Find(id);
        }

        public IEnumerable<ShowEpisode> GetAll()
        {
            return db.ShowEpisodes.ToList();
        }

        public void Update(ShowEpisode item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
