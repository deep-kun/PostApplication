using DataAccessLayer.PostService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstraction
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}