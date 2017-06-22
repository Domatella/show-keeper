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
    class EFUnitOfWork : IUnitOfWork
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

        public IRepository<Show> Shows => throw new NotImplementedException();

        public IRepository<ShowEpisode> ShowEpisodes => throw new NotImplementedException();

        public IRepository<Purchase> Purchases => throw new NotImplementedException();

        public IRepository<Subscription> Subscriptions => throw new NotImplementedException();

        public IRepository<UserSubscription> UserSubscriptions => throw new NotImplementedException();

        public IRepository<User> Users => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
