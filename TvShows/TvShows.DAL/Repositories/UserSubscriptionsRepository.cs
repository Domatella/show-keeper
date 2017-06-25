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
    public class UserSubscriptionsRepository : IRepository<UserSubscription>
    {
        private KeeperContext db;

        public UserSubscriptionsRepository(KeeperContext context)
        {
            db = context;
        }

        public void Create(UserSubscription item)
        {
            db.UserSubscriptions.Add(item);
        }

        public void Delete(int id)
        {
            UserSubscription userSubscription = db.UserSubscriptions.Find(id);
            if (userSubscription != null)
            {
                db.UserSubscriptions.Remove(userSubscription);
            }
        }

        public IEnumerable<UserSubscription> Find(Func<UserSubscription, bool> predicate)
        {
            return db.UserSubscriptions.Where(predicate).ToList();
        }

        public UserSubscription Get(int id)
        {
            return db.UserSubscriptions.Find(id);
        }

        public IEnumerable<UserSubscription> GetAll()
        {
            return db.UserSubscriptions.ToList();
        }

        public void Update(UserSubscription item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
