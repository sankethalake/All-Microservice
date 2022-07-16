using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayersMicroService.Database;

namespace MicroService2.Database
{
    public class DatabaseContext : DbContext
    {
         
        public DbSet<Player> Players { get; set; }
        public DbSet<Sport> Sports { get; set; }
       

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }

}
