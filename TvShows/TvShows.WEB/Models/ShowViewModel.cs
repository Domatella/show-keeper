using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TvShows.WEB.Models
{
    public class ShowViewModel
    {
        public int Id { get; set; }

        [StringLength(200, ErrorMessage = "Название не должно превышать 200 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Количество сезонов")]
        public int Seasons { get; set; }

        [Display(Name = "Количество серий")]
        public int Episodes { get; set; }

        [StringLength(5000, ErrorMessage = "Название не должно превышать 5000 символов")]
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}