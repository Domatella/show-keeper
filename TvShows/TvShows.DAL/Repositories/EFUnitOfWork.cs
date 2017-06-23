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
    public class EFUnitOfWork : IUnitOfWork
    {
        private KeeperContext db;

        private ShowsRepository showRepository;
        private ShowEpisodesRepository showEpisodesRepository;
        private PurchasesRepository purchasesRepository;
        private SubscriptionsRepository subscriptionsRepository;
        private UserSubscriptionsRepository userSubscriptionsRepository;
        private UsersRepository usersRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new KeeperContext(connectionString);
        }

        public IRepository<Show> Shows
        {
            get
            {
                if (showRepository == null)
                {
                    showRepository = new ShowsRepository(db);
                }

                return showRepository;
            }
        }

        public IRepository<ShowEpisode> ShowEpisodes
        {
            get
            {
                if (showEpisodesRepository == null)
                {
                    showEpisodesRepository = new ShowEpisodesRepository(db);
                }

                return showEpisodesRepository;
            }
        }

        public IRepository<Purchase> Purchases
        {
            get
            {
                if (purchasesRepository == null)
                {
                    purchasesRepository = new PurchasesRepository(db);
                }

                return purchasesRepository;
            }
        }

        public IRepository<Subscription> Subscriptions
        {
            get
            {
                if (subscriptionsRepository == null)
                {
                    subscriptionsRepository = new SubscriptionsRepository(db);
                }

                return subscriptionsRepository;
            }
        }

        public IRepository<UserSubscription> UserSubscriptions
        {
            get
            {
                if (userSubscriptionsRepository == null)
                {
                    userSubscriptionsRepository = new UserSubscriptionsRepository(db);
                }

                return userSubscriptionsRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new UsersRepository(db);
                }

                return usersRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
