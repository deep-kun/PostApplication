using DataAccessLayer.PostService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Abstraction;

namespace DataAccessLayer.DataBaseImpelemtation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly PostServiceContext postServiceContext;

        public RoleRepository(PostServiceContext postServiceContext)
        {
            this.postServiceContext = postServiceContext;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await this.postServiceContext.Roles.AsNoTracking().ToListAsync();
        }
    }
}
