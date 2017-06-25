using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.BLL.DTO
{
    public class UserShowsViewDTO
    {
        public int UserId { get; set; }
        public List<UserShowDTO> UserShowsList { get; set; }

        public UserShowsViewDTO()
        {
            UserShowsList = new List<UserShowDTO>();
        }
    }

    public class UserShowDTO
    {
        public int ShowId { get; set; }
        public int ShowEpisodeId { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }
    }
}
