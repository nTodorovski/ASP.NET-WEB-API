using DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModels
{
    public class LottoContext : DbContext
    {
        private readonly string _connectionString;

        public LottoContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LottoContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Ticket>()
                .HasOne(x => x.User)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x => x.UserId);

            var users = new List<User>
            {
                new User(){
                    Id = 1,
                    Username = "risto@gmail.com",
                    FirstName = "Risto",
                    LastName = "Panchevski",
                    Password = "P@ssw0rd",
                    Balance = 1000,
                    Role = RoleEnum.Admin,
                    Tickets = new List<Ticket>()
                }
            };

            builder.Entity<User>().HasData(users);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<RoundResult> RoundResults { get; set; }
    }
}
