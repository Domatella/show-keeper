using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.DAL.Entities
{
    public class ShowEpisode
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int ShowId { get; set; }

        public int Season { get; set; }
        public int Episode { get; set; }
    }
}
