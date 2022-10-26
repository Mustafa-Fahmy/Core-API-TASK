using DomainLayer.Models;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomService
{
    public interface IAuthenticationService
    {
         Task<AuthReturn> Login( Login model);
        Task<RegisterReturn> Register(RegisterModel model);
    }
}
