using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.BLL.DTO
{
    public class BasketDTO
    {
        public int UserId { get; set; }
        public int PurchaseId { get; set; }
        public List<SubscriptionDTO> SubscriptionsList { get; set; }

        public BasketDTO()
        {
            SubscriptionsList = new List<SubscriptionDTO>();
        }
    }


}
