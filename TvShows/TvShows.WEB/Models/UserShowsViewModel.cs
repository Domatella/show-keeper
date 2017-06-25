using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TvShows.WEB.Models
{
    public class UserShowsViewModel
    {
        public int UserId { get; set; }
        public List<UserShow> UserShowsList { get; set; }

        public UserShowsViewModel()
        {
            UserShowsList = new List<UserShow>();
        }
    }

    public class UserShow
    {
        public int ShowId { get; set; }
        public int ShowEpisodeId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Сезон")]
        public int Season { get; set; }

        [Display(Name = "Серия")]
        public int Episode { get; set; }
    }
}