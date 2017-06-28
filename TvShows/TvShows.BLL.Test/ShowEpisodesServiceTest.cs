using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TvShows.BLL.Services;
using TvShows.BLL.DTO;
using Moq;
using TvShows.DAL.Interfaces;
using TvShows.DAL.Entities;
using System.Collections.Generic;

namespace TvShows.BLL.Test
{
    [TestClass]
    public class ShowEpisodesServiceTest
    {
        private ShowEpisodesService service;

        [TestMethod]
        public void ShowEpisodesService_Create_calls_Create_method()
        {
            var showEpisode = new ShowEpisodeDTO
            {
                Id = 456,
                Episode = 2,
                Season = 3,
                ShowId = 4,
                UserId = 78
            };

            bool isCreateCalled = false;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.ShowEpisodes.Create(It.Is<ShowEpisode>(se =>
                (se.Id == showEpisode.Id) &&
                (se.Episode == showEpisode.Episode) &&
                (se.Season == showEpisode.Season) &&
                (se.ShowId == showEpisode.ShowId) &&
                (se.UserId == showEpisode.UserId)))).Callback(() => isCreateCalled = true);

            service = new ShowEpisodesService(mock.Object);
            service.Create(showEpisode);

            Assert.IsTrue(isCreateCalled);
        }

        [TestMethod]
        public void ShowEpisodesService_Delete_calls_Delete_method()
        {
            var id = 25;

            bool isDeleteCalled = false;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.ShowEpisodes.Delete(id)).Callback(() => isDeleteCalled = true);

            service = new ShowEpisodesService(mock.Object);
            service.Delete(id);

            Assert.IsTrue(isDeleteCalled);
        }

        [TestMethod]
        public void ShowEpisodesService_Dispose_calls_Dispose_method()
        {
            var mock = new Mock<IUnitOfWork>();
            bool isDisposeCalled = false;
            mock.Setup(a => a.Dispose()).Callback(() => isDisposeCalled = true);

            service = new ShowEpisodesService(mock.Object);

            service.Dispose();

            Assert.IsTrue(isDisposeCalled);
        }

        [TestMethod]
        public void ShowEpisodesService_GetShowEpisode_result_not_null()
        {
            var id = 25;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.ShowEpisodes.Get(id)).Returns(new ShowEpisode());

            service = new ShowEpisodesService(mock.Object);
            var result = service.GetShowEpisode(id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowEpisodesService_GetUserShows_result_not_null()
        {
            var id = 25;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.ShowEpisodes.GetAll()).Returns(new List<ShowEpisode>());
            mock.Setup(a => a.Shows.GetAll()).Returns(new List<Show>());

            service = new ShowEpisodesService(mock.Object);
            var result = service.GetUsersShows(id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowEpisodesService_GetUserShows_returns_only_users_shows()
        {
            var id = 25;
            var mock = new Mock<IUnitOfWork>();

            mock.Setup(a => a.ShowEpisodes.GetAll()).Returns(new List<ShowEpisode>
            {
                new ShowEpisode { Id = 1, UserId = 1, Episode = 1, Season = 1, ShowId = 1 },
                new ShowEpisode { Id = 2, UserId = 5, Episode = 2, Season = 2, ShowId = 1 },
                new ShowEpisode { Id = 3, UserId = 17, Episode = 3, Season = 5, ShowId = 4 },
                new ShowEpisode { Id = 4, UserId = id, Episode = 7, Season = 1, ShowId = 8 },
                new ShowEpisode { Id = 5, UserId = id, Episode = 2, Season = 2, ShowId = 1 },
            });

            mock.Setup(a => a.Shows.GetAll()).Returns(new List<Show>
            {
                new Show { Id = 1, Name = "sdfsdf", Description = "sdfdf", Episodes = 4, Seasons = 2 },
                new Show { Id = 4, Name = "sdsfs", Description = "sqweqw", Episodes = 78, Seasons = 10 },
                new Show { Id = 8, Name = "ssdfsqr", Description = "ssdf", Episodes = 741, Seasons = 50 }
            });

            service = new ShowEpisodesService(mock.Object);
            var result = service.GetUsersShows(id);

            var expectedCount = 2;

            Assert.AreEqual(result.UserShowsList.Count, expectedCount);
        }

        [TestMethod]
        public void ShowEpisodesService_Update_calls_Update_method()
        {
            var showEpisode = new ShowEpisodeDTO
            {
                Id = 1,
                ShowId = 2,
                Episode = 7,
                Season = 4,
                UserId = 5
            };

            bool isUpdateCalled = false;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.ShowEpisodes.Update(It.Is<ShowEpisode>(se =>
                (se.Id == showEpisode.Id) &&
                (se.ShowId == showEpisode.ShowId) &&
                (se.Episode == showEpisode.Episode) &&
                (se.Season == showEpisode.Season) &&
                (se.UserId == showEpisode.UserId)))).Callback(() => isUpdateCalled = true);

            service = new ShowEpisodesService(mock.Object);
            service.Update(showEpisode);

            Assert.IsTrue(isUpdateCalled);
        }

        public void ShowEpisodesService_GetShow_result_not_null()
        {
            var id = 14;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Shows.Get(id)).Returns(new Show());

            service = new ShowEpisodesService(mock.Object);
            var result = service.GetShow(id);

            Assert.IsNotNull(result);
        }
    }
}
