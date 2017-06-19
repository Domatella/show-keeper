using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShows.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string UserId { get; set; }
        public List<int> Items { get; set; }
        public bool IsPaid { get; set; }
    }
}