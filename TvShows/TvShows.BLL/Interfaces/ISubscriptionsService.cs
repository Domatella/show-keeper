﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;

namespace TvShows.BLL.Interfaces
{
    public interface ISubscriptionsService
    {
        SubscriptionDTO GetSubscription(int? id);
        IEnumerable<SubscriptionDTO> GetSubscriptions();
        BucketDTO GetBucket(int? userId);
        void Create(SubscriptionDTO subscription);
        void Update(SubscriptionDTO subscription);
        void Delete(int id);
        void Dispose();
    }
}
