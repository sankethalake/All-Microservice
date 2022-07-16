using JwtAuthentication.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication
{
    public interface IJwtAuthenticationManager
    {
        string GenerateToken(string username, string password);
    }
}
