using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.DAL.Entities;

namespace TvShows.DAL.EF
{
    public class KeeperContext : DbContext
    {
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<ShowEpisode> ShowEpisodes { get; set; }
        public DbSet<User> Users { get; set; }

        public KeeperContext(string connectionString)
            : base(connectionString) { }
    }
}
