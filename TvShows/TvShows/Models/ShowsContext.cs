using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TvShows.Models
{
    public class ShowsContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }
        public DbSet<ShowEpisode> ShowEpisodes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
    }
}