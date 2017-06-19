using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShows.Models
{
    public class PageIndexViewModel
    {
        public IEnumerable<Show> Shows { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}