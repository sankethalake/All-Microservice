using JwtAuthentication.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JwtAuthentication.Database.Login;

namespace JwtAuthentication.Repositories
{
    public class LoginManager : IRepository<Login>
    {
        private readonly LoginDatabaseContext _loginDatabaseContext;

        public LoginManager(LoginDatabaseContext loginDatabaseContext)
        {
            _loginDatabaseContext = loginDatabaseContext;
        }

        private bool IsUserAlreadyPresent(string username)
        {
            var users = _loginDatabaseContext.Logins.Where(u => u.Username == username).ToList();
            if (users.FirstOrDefault(l => l.Username == username) != null) return true;
            return false;
        }

        public Login CreateLogin(Login login)
        {
            Login tempLogin = null;
            if (!IsUserAlreadyPresent(login.Username)){
                _loginDatabaseContext.Logins.Add(login);
                _loginDatabaseContext.SaveChanges();
                tempLogin = login;
            }
            return tempLogin;
        }

        public bool Get(Login User)
        {
            var users = _loginDatabaseContext.Logins.Where(u => u.Username == User.Username && u.Password == User.Password).ToList();
            return users.Any(u => u.Username == User.Username && u.Password == User.Password);
        }

        public IEnumerable<Login> GetAll()
        {
            return _loginDatabaseContext.Logins.ToList();
        }
    }
}
