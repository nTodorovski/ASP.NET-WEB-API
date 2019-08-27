using DomainModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class RoundResultRepository : IRepository<RoundResult>
    {
        private readonly string _connectionString;


        public RoundResultRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ToDoDatabase");
        }

        public void Add(RoundResult entity)
        {
            using (var dbContext = new LottoContext(_connectionString))
            {
                dbContext.RoundResults.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(RoundResult entity)
        {
            using (var dbContext = new LottoContext(_connectionString))
            {
                dbContext.RoundResults.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<RoundResult> GetAll()
        {
            using (var dbContext = new LottoContext(_connectionString))
            {
                return dbContext.RoundResults.ToList();
            }
        }

        public RoundResult GetById(int id)
        {
            using (var dbContext = new LottoContext(_connectionString))
            {
                return dbContext.RoundResults.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Update(RoundResult entity)
        {
            using (var dbContext = new LottoContext(_connectionString))
            {
                dbContext.RoundResults.Update(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
