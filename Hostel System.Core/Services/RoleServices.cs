using Hostel_System.Core.IServices;
using Hostel_System.Database;
using Hostel_System.Database.Entity;

namespace Hostel_System.Core.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly HostelSystemDbContext _hostelSystemDbContext;

        public RoleServices(HostelSystemDbContext hostelSystemDbContext)
        {
            _hostelSystemDbContext = hostelSystemDbContext;
        }
        public Role GetDefaultRole()
        {
            return _hostelSystemDbContext.Roles.FirstOrDefault();
        }

        public Role GetRoleByNamme(string roleName)
        {
            return _hostelSystemDbContext
                .Roles
                .FirstOrDefault(x => x.RoleName.Contains(roleName));
        }
    }
}
