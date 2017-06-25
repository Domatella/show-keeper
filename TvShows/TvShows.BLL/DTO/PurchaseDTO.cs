using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.BLL.DTO
{
    public class PurchaseDTO
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public bool IsPaid { get; set; }
    }
}
