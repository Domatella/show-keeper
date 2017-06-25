using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.DAL.Entities;

namespace TvShows.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Show> Shows { get; }
        IRepository<ShowEpisode> ShowEpisodes { get; }
        IRepository<Purchase> Purchases { get; }
        IRepository<Subscription> Subscriptions { get; }
        IRepository<UserSubscription> UserSubscriptions { get; }
        void Save();
    }
}
