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
    public class SubscriptionsRepository : IRepository<Subscription>
    {
        private KeeperContext db;

        public SubscriptionsRepository(KeeperContext context)
        {
            db = context;
        }

        public void Create(Subscription item)
        {
            db.Subscriptions.Add(item);
        }

        public void Delete(int id)
        {
            Subscription subscription = db.Subscriptions.Find(id);
            if (subscription != null)
            {
                db.Subscriptions.Remove(subscription);
            }
        }

        public IEnumerable<Subscription> Find(Func<Subscription, bool> predicate)
        {
            return db.Subscriptions.Where(predicate).ToList();
        }

        public Subscription Get(int id)
        {
            return db.Subscriptions.Find(id);
        }

        public IEnumerable<Subscription> GetAll()
        {
            return db.Subscriptions.ToList();
        }

        public void Update(Subscription item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
