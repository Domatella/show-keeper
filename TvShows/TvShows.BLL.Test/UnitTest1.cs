using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TvShows.BLL.Services;
using Moq;
using System.Collections.Generic;
using TvShows.DAL.Interfaces;
using TvShows.DAL.Entities;

namespace TvShows.BLL.Test
{
    [TestClass]
    public class ShowsServiceTest
    {
        private ShowsService service;

        [TestMethod]
        public void ShowsService_GetShows_result_not_null()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Shows.GetAll()).Returns(new List<Show>());
            service = new ShowsService(mock.Object);

            var result = service.GetShows();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowsService_GetShow_result_not_null()
        {
            var show = 1;

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Shows.Get(show)).Returns(new Show());
            service = new ShowsService(mock.Object);

            var result = service.GetShow(show);

            Assert.IsNotNull(result);
        }
    }
}
