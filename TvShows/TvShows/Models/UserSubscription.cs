using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShows.Models
{
    public class UserSubscription
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        public string UserId { get; set; }
        public int PurchaseId { get; set; }
        public int SubscriptionId { get; set; }
    }
}