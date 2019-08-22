using System;
using System.Collections.Generic;
using System.Text;
using Data;
using DomainModels;
using Models;

namespace Business
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<User> _userRepository;

        public TicketService(IRepository<Ticket> ticketRepository, IRepository<User> userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }

        public void CreateTicket(int id,string combination)
        {
            var user = _userRepository.GetById(id);
            var ticket = new Ticket()
            {
                Combination = combination,
                UserId = id,
                //ROUND GET ALL 
                // DRAW
            };
        }

        public IEnumerable<TicketModel> GetAllByUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketModel> GetAllTickets()
        {
            throw new NotImplementedException();
        }
    }
}
