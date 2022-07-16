using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Database
{
    [Table("Login")]
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage ="Username is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        public class LoginDatabaseContext : DbContext
        {
            public LoginDatabaseContext(DbContextOptions options) : base(options)
            {

            }
            public DbSet<Login> Logins { get; set; }
        }
    }
}
