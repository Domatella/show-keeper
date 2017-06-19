using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TvShows.Startup))]
namespace TvShows
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
