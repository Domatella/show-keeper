using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TvShows.BLL.Services;
using Moq;
using System.Collections.Generic;
using TvShows.DAL.Interfaces;
using TvShows.DAL.Entities;
using TvShows.BLL.DTO;

namespace TvShows.BLL.Test
{
    [TestClass]
    public class ShowsServiceTest
    {
        private ShowsService service;

        [TestMethod]
        public void ShowsService_Create_calls_Create_method()
        {
            var showDto = new ShowDTO
            {
                Id = 100,
                Description = "sdf",
                Name = "dfg",
                Episodes = 5,
                Seasons = 2
            };

            var mock = new Mock<IUnitOfWork>();
            bool isCreateCalled = false;
            mock.Setup(a => a.Shows.Create(It.Is<Show>(s => 
                (s.Id == showDto.Id) &&
                (s.Name == showDto.Name) &&
                (s.Seasons == showDto.Seasons) &&
                (s.Episodes == showDto.Episodes) &&
                (s.Description == showDto.Description)))).Callback(() => isCreateCalled = true);

            service = new ShowsService(mock.Object);

            service.Create(showDto);
 
            Assert.IsTrue(isCreateCalled);
        }

        [TestMethod]
        public void ShowsService_Delete_calls_Delete_method()
        {
            var id = 25;
            var mock = new Mock<IUnitOfWork>();
            bool isDeleteCalled = false;
            mock.Setup(a => a.Shows.Delete(id)).Callback(() => isDeleteCalled = true);

            service = new ShowsService(mock.Object);

            service.Delete(id);

            Assert.IsTrue(isDeleteCalled);
        }

        [TestMethod]
        public void ShowsService_Dispose_calls_Dispose_method()
        {
            var mock = new Mock<IUnitOfWork>();
            bool isDisposeCalled = false;
            mock.Setup(a => a.Dispose()).Callback(() => isDisposeCalled = true);

            service = new ShowsService(mock.Object);

            service.Dispose();

            Assert.IsTrue(isDisposeCalled);
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
        public void ShowsService_Update_calls_Update_method()
        {
            var showDto = new ShowDTO
            {
                Id = 100,
                Description = "sdf",
                Name = "dfg",
                Episodes = 5,
                Seasons = 2
            };

            var mock = new Mock<IUnitOfWork>();
            bool isUpdateCalled = false;
            mock.Setup(a => a.Shows.Update(It.Is<Show>(s =>
                (s.Id == showDto.Id) &&
                (s.Name == showDto.Name) &&
                (s.Seasons == showDto.Seasons) &&
                (s.Episodes == showDto.Episodes) &&
                (s.Description == showDto.Description)))).Callback(() => isUpdateCalled = true);

            service = new ShowsService(mock.Object);

            service.Update(showDto);

            Assert.IsTrue(isUpdateCalled);
        }
    }
}
