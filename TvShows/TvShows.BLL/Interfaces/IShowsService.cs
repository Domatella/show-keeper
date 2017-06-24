using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;

namespace TvShows.BLL.Interfaces
{
    public interface IShowsEpisodeService
    {
        ShowDTO GetShow(int id);
        IEnumerable<ShowDTO> GetShows();
        void Create(ShowDTO show);
        void Delete(int id);
        void Update(ShowDTO show);
        void Dispose();
    }
}
