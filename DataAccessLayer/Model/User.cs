using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int Role { get; set; }
    }
}
