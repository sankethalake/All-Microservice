using JwtAuthentication.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        bool Get(Login User);

        Login CreateLogin(Login login);
    }
}
