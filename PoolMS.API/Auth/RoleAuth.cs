using Microsoft.AspNetCore.Authorization;
using PoolMS.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace PoolMS.API.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class RoleAuth: AuthorizeAttribute
    {
       public string Role { get; set; }
    }
}
