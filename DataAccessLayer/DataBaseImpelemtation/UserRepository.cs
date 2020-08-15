using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DataAccessLayer.Abstraction;
using DataAccessLayer.PostService;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBaseImpelemtation
{
    public class UserRepository : IUserRepository
    {
        private readonly PostServiceContext postServiceContext;

        public UserRepository(PostServiceContext postServiceContext)
        {
            this.postServiceContext = postServiceContext;
        }

        public IEnumerable<User> GetAll()
        {
            return this.postServiceContext.Users.AsNoTracking().AsEnumerable();
        }

        public User GetUserByLoginPassword(string login, string passwordHash)
        {
            return this.postServiceContext.Users
                .Where(u => u.UserLogin.Equals(login) && u.PasswordHash.Equals(passwordHash))
                .AsNoTracking()
                .SingleOrDefault();
        }

        public int RegisterUser(User u)
        {
            this.postServiceContext.Users.Add(u);
            return this.postServiceContext.SaveChanges();
        }

        public User GetUserByLogin(string nick)
        {
            return this.postServiceContext.Users.Where(u => u.UserLogin.Equals(nick)).AsNoTracking().SingleOrDefault();
        }
    }
}
