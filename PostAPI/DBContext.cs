using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAPI
{
    public class DBContext : IDBContext
    {
        public DBContext(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public string ConnectionString { get; }
    }
}
