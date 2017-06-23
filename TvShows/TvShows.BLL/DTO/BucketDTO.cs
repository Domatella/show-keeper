using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.BLL.DTO
{
    public class BucketDTO
    {
        public int UserId { get; set; }
        public int PurchaseId { get; set; }
        public List<UserSubscriptionDTO> UserSubscriptionsList { get; set; }

        public BucketDTO()
        {
            UserSubscriptionsList = new List<UserSubscriptionDTO>();
        }
    }
}
