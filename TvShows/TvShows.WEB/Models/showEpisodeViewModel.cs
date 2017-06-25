using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TvShows.WEB.Models
{
    public class ShowEpisodeViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShowId { get; set; }

        [Display(Name = "Сезон")]
        public int Season { get; set; }

        [Display(Name = "Серия")]
        public int Episode { get; set; }
    }
}