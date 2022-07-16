using Microsoft.EntityFrameworkCore;
using ParticipationMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParticipationMicroservice.DBContexts
{
    public class ParticipationContext: DbContext
    {
        public ParticipationContext(DbContextOptions<ParticipationContext> options) : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Participation> Participations { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().HasData(
        //        new Category
        //        {
        //            Id = 1,
        //            Name = "Electronics",
        //            Description = "Electronic Items",
        //        },
        //        new Category
        //        {
        //            Id = 2,
        //            Name = "Clothes",
        //            Description = "Dresses",
        //        },
        //        new Category
        //        {
        //            Id = 3,
        //            Name = "Grocery",
        //            Description = "Grocery Items",
        //        }
        //    );
        //}
    }
}

