using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShows.WEB.Models
{
    public class ShowEpisodeViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShowId { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }
    }
}