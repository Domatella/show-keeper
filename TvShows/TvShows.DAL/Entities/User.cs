using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int Nickname { get; set; }
        public int Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
