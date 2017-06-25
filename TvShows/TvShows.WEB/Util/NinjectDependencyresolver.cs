using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TvShows.BLL.Interfaces;
using TvShows.BLL.Services;

namespace TvShows.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IShowsService>().To<ShowsService>();
            kernel.Bind<IShowEpisodesService>().To<ShowEpisodesService>();
            kernel.Bind<ISubscriptionsService>().To<SubscriptionsService>();
        }
    }
}