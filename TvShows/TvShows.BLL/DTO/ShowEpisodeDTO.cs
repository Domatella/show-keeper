using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.BLL.DTO
{
    public class ShowEpisodeDTO
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ShowId { get; set; }

        public int Season { get; set; }
        public int Episode { get; set; }
    }
}
