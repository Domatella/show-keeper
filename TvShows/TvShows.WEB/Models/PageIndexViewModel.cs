using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TvShows.WEB.Models
{
    public class PageIndexViewModel
    {
        public IEnumerable<ShowViewModel> Shows { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}