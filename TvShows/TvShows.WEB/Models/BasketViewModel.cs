using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShows.WEB.Models
{
    public class BasketViewModel
    {
        public int UserId { get; set; }
        public int PurchaseId { get; set; }
        public List<SubscriptionViewModel> SubscriptionsList { get; set; }

        public BasketViewModel()
        {
            SubscriptionsList = new List<SubscriptionViewModel>();
        }
    }
}