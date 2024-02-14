using PoolMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PoolMS.Service.Interface
{
    public interface IJwtProvider
    {
        string Generate(User user);
        
    }
}
