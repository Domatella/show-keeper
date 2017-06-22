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
    class ShowsRepository : IRepository<Show>
    {
        private KeeperContext db;

        public ShowsRepository(KeeperContext context)
        {
            db = context;
        }

        public void Create(Show item)
        {
            db.Shows.Add(item);
        }

        public void Delete(int id)
        {
            Show show = db.Shows.Find(id);
            if (show != null)
            {
                db.Shows.Remove(show);
            }
        }

        public IEnumerable<Show> Find(Func<Show, bool> predicate)
        {
            return db.Shows.Where(predicate).ToList();
        }

        public void Update(Show item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public Show Get(int id)
        {
            return db.Shows.Find(id);
        }

        public IEnumerable<Show> GetAll()
        {
            return db.Shows.ToList();
        }
    }
}
