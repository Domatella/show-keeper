using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TvShows.Models
{
    public class ShowEpisode
    {
        public int ShowEpisodeId { get; set; }
        public string UserId { get; set; }
        public int ShowId { get; set; }

        [Display(Name = "Сезон")]
        public int Season { get; set; }

        [Display(Name = "Серия")]
        public int Episode { get; set; }
    }
}