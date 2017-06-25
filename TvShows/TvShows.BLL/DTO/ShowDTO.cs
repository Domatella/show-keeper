using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.BLL.DTO
{
    public class ShowDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Seasons { get; set; }

        [Required]
        public int Episodes { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }
    }
}
