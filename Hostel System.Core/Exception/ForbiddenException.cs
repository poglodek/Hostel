using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel_System.Core.Exception
{
    public class ForbiddenException : System.Exception
    {
        public ForbiddenException(string message) : base(message)
        {
            
        }
    }
}
