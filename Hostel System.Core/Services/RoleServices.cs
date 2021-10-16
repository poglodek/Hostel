using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

    }
}
