using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.DAL.Entities
{
    public class UserSubscription
    {
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }
        public int SubscriptionId { get; set; }
    }
}
