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

        static KeeperContext()
        {
            Database.SetInitializer<KeeperContext>(new StoreDbInitializer());
        }

        public KeeperContext(string connectionString)
            : base(connectionString) { }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<KeeperContext>
    {
        protected override void Seed(KeeperContext context)
        {
            context.Shows.Add(new Show { Name = "Доктор Кто", Seasons = 10, Episodes = 100, Description = "Сериал о путешественнике во времени и пространстве" });
            context.Shows.Add(new Show { Name = "Бесстыжие", Seasons = 7, Episodes = 84, Description = "Сериал о многодетной семье с папашей-алкоголиком во главе" });
            context.Shows.Add(new Show { Name = "Черное зеркало", Seasons = 4, Episodes = 13, Description = "Сериал-сатира о ближайшем будущем человечества, на которое повлияли информационные технологии" });
            context.Shows.Add(new Show { Name = "Гравити Фоллз", Seasons = 2, Episodes = 40, Description = "Мультсериал о близнецых Диппере и Мэйбл, которые гостят в таинственном городке Гравити Фолз" });
            context.Shows.Add(new Show { Name = "Рик и Морти", Seasons = 3, Episodes = 22, Description = "Мультсериал о безумном ученом и его внуке" });
            context.Shows.Add(new Show { Name = "Убийство на пляже", Seasons = 3, Episodes = 24, Description = "Детективный сериал" });

            context.Subscriptions.Add(new Subscription { Name = "Netflix", ImageUrl = "Content/wuzyVTTX.jpg", Price = 100.00M });
            context.Subscriptions.Add(new Subscription { Name = "BBC", ImageUrl = "Content/photo.jpg", Price = 90.00M });
            context.Subscriptions.Add(new Subscription { Name = "Showtime", ImageUrl = "Content/LJm4KXdt.jpg", Price = 80.00M });

            context.SaveChanges();
        }
    }
}
