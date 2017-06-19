using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace TvShows.Models
{
    public class Show
    {
        public int ShowId { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Количество сезонов")]
        public int Seasons { get; set; }

        [Required]
        [Display(Name = "Количество серий")]
        public int Series { get; set; }

        [Display(Name = "Год выпуска")]
        public int Year { get; set; }

        [Display(Name = "Описание")]
        [StringLength(5000, ErrorMessage ="Описание не должно превышать 5000 символов")]
        public string Description { get; set; }
    }
}