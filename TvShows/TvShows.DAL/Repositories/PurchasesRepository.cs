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
    public class PurchasesRepository : IRepository<Purchase>
    {
        private KeeperContext db;

        public PurchasesRepository(KeeperContext context)
        {
            db = context;
        }

        public void Create(Purchase item)
        {
            db.Purchases.Add(item);
        }

        public void Delete(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            if (purchase != null)
            {
                db.Purchases.Remove(purchase);
            }
        }

        public IEnumerable<Purchase> Find(Func<Purchase, bool> predicate)
        {
            return db.Purchases.Where(predicate).ToList();
        }

        public Purchase Get(int id)
        {
            return db.Purchases.Find(id);
        }

        public IEnumerable<Purchase> GetAll()
        {
            return db.Purchases.ToList();
        }

        public void Update(Purchase item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
