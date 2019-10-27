using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IDBContext
    {
        string ConnectionString { get; }
    }
}
