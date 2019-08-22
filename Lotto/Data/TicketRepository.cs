using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class TicketRepository : IRepository<Ticket>
    {
        public void Add(Ticket entity)
        {
            using (var dbContext = new LottoContext())
            {
                dbContext.Tickets.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Ticket entity)
        {
            using (var dbContext = new LottoContext())
            {
                dbContext.Tickets.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Ticket> GetAll()
        {
            using (var dbContext = new LottoContext())
            {
                return dbContext.Tickets.ToList();
            }
        }

        public Ticket GetById(int id)
        {
            using (var dbContext = new LottoContext())
            {
                return dbContext.Tickets.FirstOrDefault(x => x.Id == id);
            }
        }

        public void Update(Ticket entity)
        {
            using (var dbContext = new LottoContext())
            {
                dbContext.Tickets.Update(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
