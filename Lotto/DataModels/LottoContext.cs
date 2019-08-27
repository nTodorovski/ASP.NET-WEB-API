using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModels
{
    public class LottoContext : DbContext
    {
        //private readonly string _connectionString;
        //public LottoContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Database=LottoDb;User Id=SA;Password=Password1;");
            //options.UseSqlServer("Server=PETRA05;Database=LottoDb;User Id=SA;Password=Password1;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Ticket>()
            //    .HasOne<User>()
            //    .WithMany(x => x.Tickets)
            //    .HasForeignKey(x => x.UserId);

            builder.Entity<Ticket>()
                .HasOne(x => x.User)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.UserId);

            //builder.Entity<User>()
            //    .HasMany<Ticket>()
            //    .WithOne(x => x.User)
            //    .HasForeignKey(x => x.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<RoundResult> RoundResults { get; set; }
    }
}
