using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAPI
{
    public class DBContext : IDBContext
    {
        public string ConnectionString => @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=PostService;Integrated Security=yes";
    }
}
