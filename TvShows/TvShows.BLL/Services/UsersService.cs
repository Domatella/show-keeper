using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvShows.BLL.DTO;
using TvShows.BLL.Interfaces;
using TvShows.DAL.Entities;
using TvShows.DAL.Interfaces;

namespace TvShows.BLL.Services
{
    public class UsersService : IUsersService
    {
        private IUnitOfWork db { get; set; }

        public UsersService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public void Create(UserDTO user)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User>());
            db.Users.Create(Mapper.Map<User>(user));
            db.Save();
        }

        public void Delete(int id)
        {
            db.Users.Delete(id);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public UserDTO GetUser(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            return Mapper.Map<UserDTO>(db.Users.Get(id));
        }

        public void Update(UserDTO user)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User>());
            db.Users.Update(Mapper.Map<User>(user));
            db.Save();
        }
    }
}
