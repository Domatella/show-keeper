using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TvShows.WEB.Models
{
    public class SubscriptionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}