using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.BLL.DTO
{
    public class UserSubscriptionDTO
    {
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }
        public int SubscriptionId { get; set; }
    }
}
