using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;

namespace TvShows.BLL.Interfaces
{
    public interface IUsersService
    {
        UserDTO GetUser(int? id);
        void Create(UserDTO user);
        void Delete(int id);
        void Update(UserDTO user);
        void Dispose();
    }
}
