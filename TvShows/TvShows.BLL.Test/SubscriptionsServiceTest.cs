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
    public class SubscriptionsServiceTest
    {
        private SubscriptionsService service;

        [TestMethod]
        public void SubscriptionsService_Create_calls_Create_method()
        {
            var subscription = new SubscriptionDTO
            {
                Id = 1,
                ImageUrl = "sdfs",
                Name = "sdf",
                Price = 41
            };

            bool isCreateCalled = false;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Subscriptions.Create(It.Is<Subscription>(s =>
                (s.Id == subscription.Id) &&
                (s.ImageUrl == subscription.ImageUrl) &&
                (s.Name == subscription.Name) &&
                (s.Price == subscription.Price)))).Callback(() => isCreateCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.Create(subscription);

            Assert.IsTrue(isCreateCalled);
        }

        [TestMethod]
        public void SubscriptionsService_Delete_calls_Delete_method()
        {
            int id = 14;
            bool isDeleteCalled = false;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Subscriptions.Delete(id)).Callback(() => isDeleteCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.Delete(id);

            Assert.IsTrue(isDeleteCalled);
        }

        [TestMethod]
        public void SubscriptionsService_Dispose_calls_Dispose_method()
        {
            bool isDisposeCalled = false;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Dispose()).Callback(() => isDisposeCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.Dispose();

            Assert.IsTrue(isDisposeCalled);
        }

        [TestMethod]
        public void SubscriprionsService_GetBasket_result_not_null()
        {
            int userId = 14;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Purchases.Get(userId)).Returns(new Purchase());
            mock.Setup(a => a.UserSubscriptions.GetAll()).Returns(new List<UserSubscription>());
            mock.Setup(a => a.Subscriptions.GetAll()).Returns(new List<Subscription>());

            service = new SubscriptionsService(mock.Object);
            var result = service.GetBasket(userId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubscriptionsService_GetBucket_returns_only_current_subscriptions()
        {
            int userId = 14;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Purchases.Get(userId)).Returns(new Purchase
            {
                Id = 4,
                IsPaid = false,
                UserId = userId
            });

            mock.Setup(a => a.UserSubscriptions.GetAll()).Returns(new List<UserSubscription>
            {
                new UserSubscription { Id = 1, PurchaseId = 2, SubscriptionId = 5 },
                new UserSubscription { Id = 2, PurchaseId = 1, SubscriptionId = 8 },
                new UserSubscription { Id = 3, PurchaseId = 4, SubscriptionId = 5 },
                new UserSubscription { Id = 4, PurchaseId = 4, SubscriptionId = 7 }
            });

            mock.Setup(a => a.Subscriptions.GetAll()).Returns(new List<Subscription>
            {
                new Subscription { Id = 5, ImageUrl = "sdf", Name = "djf", Price = 74 },
                new Subscription { Id = 6, ImageUrl = "asd", Name = "asd", Price = 85 },
                new Subscription { Id = 7, ImageUrl = "dfvd", Name = "ert", Price = 12 }
            });

            service = new SubscriptionsService(mock.Object);
            var result = service.GetBasket(userId);
            var expected = 2;

            Assert.AreEqual(result.SubscriptionsList.Count, expected);
        }

        [TestMethod]
        public void SubscriptionsService_GetSubscription_result_not_null()
        {
            int id = 85;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Subscriptions.Get(id)).Returns(new Subscription());

            service = new SubscriptionsService(mock.Object);
            var result = service.GetSubscription(id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubscriptionsService_Update_calls_Update_method()
        {
            var subscription = new SubscriptionDTO
            {
                Id = 5,
                ImageUrl = "sdf",
                Name = "asd",
                Price = 75
            };

            var isUpdateCalled = false;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Subscriptions.Update(It.Is<Subscription>(s =>
                (s.Id == subscription.Id) &&
                (s.ImageUrl == subscription.ImageUrl) &&
                (s.Name == subscription.Name) &&
                (s.Price == subscription.Price)))).Callback(() => isUpdateCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.Update(subscription);

            Assert.IsTrue(isUpdateCalled);
        }

        [TestMethod]
        public void SubscriptionsService_GetAll_result_not_null()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Subscriptions.GetAll()).Returns(new List<Subscription>());

            service = new SubscriptionsService(mock.Object);
            var result = service.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubscriptionsService_AddUserSubscription_calls_Create_method()
        {
            int purchaseId = 74;
            int subscriptionId = 12;
            bool isCreateCalled = false;

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.UserSubscriptions.Create(It.Is<UserSubscription>(us =>
                (us.PurchaseId == purchaseId) &&
                (us.SubscriptionId == subscriptionId)))).Callback(() => isCreateCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.AddUserSubscription(purchaseId, subscriptionId);

            Assert.IsTrue(isCreateCalled);
        }

        [TestMethod]
        public void SubscriptionsService_AddPurchase_calls_Create_method()
        {
            int userId = 74;
            bool isCreateCalled = false;

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Purchases.Create(It.Is<Purchase>(p =>
                (p.UserId == userId) &&
                (p.IsPaid == false)))).Callback(() => isCreateCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.AddPurchase(userId);

            Assert.IsTrue(isCreateCalled);
        }

        [TestMethod]
        public void SubscriptionsService_DeletePurchase_calls_Delete_method()
        {
            int id = 12;
            bool isDeleteCalled = false;

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Purchases.Delete(id)).Callback(() => isDeleteCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.DeletePurchase(id);

            Assert.IsTrue(isDeleteCalled);
        }

        [TestMethod]
        public void SubscriptionsService_DeleteUserSubscription_calls_Delete_method()
        {
            int purchaseId = 74;
            int subscriptionId = 17;
            bool isDeleteCalled = false;

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.UserSubscriptions.Find(It.IsAny<Func<UserSubscription, bool>>()))
                .Returns(new List<UserSubscription>
                {
                    new UserSubscription { Id = 21, PurchaseId = purchaseId, SubscriptionId = subscriptionId }
                });
            mock.Setup(a => a.UserSubscriptions.Delete(It.IsAny<int>())).Callback(() => isDeleteCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.DeleteUserSubscription(purchaseId, subscriptionId);

            Assert.IsTrue(isDeleteCalled);
        }

        [TestMethod]
        public void SubscriptionsService_UpdatePurchase_calls_Update_method()
        {
            var purchase = new PurchaseDTO
            {
                Id = 7,
                IsPaid = false,
                UserId = 12
            };
            bool isUpdateCalled = false;

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Purchases.Update(It.Is<Purchase>(p =>
                (p.Id == purchase.Id) &&
                (p.IsPaid == purchase.IsPaid) &&
                (p.UserId == purchase.UserId)))).Callback(() => isUpdateCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.UpdatePurchase(purchase);

            Assert.IsTrue(isUpdateCalled);
        }

        [TestMethod]
        public void SubscriptionsService_GetCurrentPurchase_result_not_null()
        {
            int id = 85;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Purchases.Get(id)).Returns(new Purchase());

            service = new SubscriptionsService(mock.Object);
            var result = service.GetCurrentPurchase(id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubscriptionsService_PayPurchase_calls_Update_method()
        {
            int id = 12;
            bool isUpdateCalled = true;
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(a => a.Purchases.Find(It.IsAny<Func<Purchase, bool>>())).Returns(new List<Purchase>
            {
                new Purchase { Id = id, IsPaid = false, UserId = 1 }
            });
            mock.Setup(a => a.Purchases.Update(It.Is<Purchase>(p =>
                (p.Id == id) &&
                (p.IsPaid == true) &&
                (p.UserId == 1)))).Callback(() => isUpdateCalled = true);

            service = new SubscriptionsService(mock.Object);
            service.PayPurchase(id);

            Assert.IsTrue(isUpdateCalled);
        }
    }
}
