using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;

namespace TvShows.BLL.Interfaces
{
    public interface IShowEpisodesService
    {
        ShowEpisodeDTO GetShowEpisode(int? id);
        UserShowsViewDTO GetUsersShows(int? userId);
        void Create(ShowEpisodeDTO showEpisode);
        void Delete(int id);
        void Update(ShowEpisodeDTO showEpisode);
        void Dispose();
    }
}
