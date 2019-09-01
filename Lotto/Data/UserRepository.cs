using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class UserRepository : IRepository<User>
    {
        //private readonly string _connectionString;


        //public UserRepository(IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("ToDoDatabase");
        //}

        public void Add(User entity)
        {
            using (var dbContext = new LottoContext())
            {
                dbContext.Users.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(User entity)
        {
            using (var dbContext = new LottoContext())
            {
                dbContext.Users.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var dbContext = new LottoContext())
            {
                return dbContext.Users.ToList();
            }
        }

        public User GetById(int id)
        {
            using (var dbContext = new LottoContext())
            {
                return dbContext.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Update(User entity)
        {
            using (var dbContext = new LottoContext())
            {
                dbContext.Users.Update(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
