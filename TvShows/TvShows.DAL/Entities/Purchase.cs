using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.DAL.Entities
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public int ItemsQuantity { get; set; }
        public bool IsPaid { get; set; }
    }
}
