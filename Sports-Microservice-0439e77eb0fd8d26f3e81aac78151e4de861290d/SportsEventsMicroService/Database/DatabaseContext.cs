using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsEventsMicroService.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}
