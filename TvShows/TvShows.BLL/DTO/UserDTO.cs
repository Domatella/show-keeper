using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShows.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int Nickname { get; set; }
        public int Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
