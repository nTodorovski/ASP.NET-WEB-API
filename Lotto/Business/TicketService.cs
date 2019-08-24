using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Data;
using DomainModels;
using DomainModels.Enum;
using Models;

namespace Business
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RoundResult> _roundResultRepository;


        public TicketService(IRepository<Ticket> ticketRepository, IRepository<User> userRepository, IRepository<RoundResult> roundResultRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _roundResultRepository = roundResultRepository;

        }

        public void CreateTicket(int id, TicketModel model)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                throw new Exception("User not found!");

            if (user.Balance <= 50)
                throw new Exception("You don't have enough money to place a ticket!");

            var nextRound = _roundResultRepository.GetAll().Max(x => x.RoundId) + 1;

            Regex regex = new Regex("^[0-9]{2}[,][0-9]{2}[,][0-9]{2}[,][0-9]{2}[,][0-9]{2}[,][0-9]{2}[,][0-9]{2}$");
            var match = regex.Match(model.Combination);
            if (!match.Success)
                throw new Exception("Invalid combination");

            // TODO: different numbers in combination
            var ticket = new Ticket()
            {
                Combination = model.Combination,
                UserId = id,
                Status = StatusEnum.Pending,
                AwardBalance = 0,
                Round = nextRound
            };

            user.Balance = user.Balance - 50;
            _userRepository.Update(user);

            _ticketRepository.Add(ticket);
        }

        public IEnumerable<TicketModel> GetAllByUser(int id)
        {
            return _ticketRepository
                .GetAll()
                .Where(x => x.UserId == id)
                .Select(x => new TicketModel {
                    Combination = x.Combination,
                    Round = x.Round,
                    Status = x.Status,
                    AwardBalance = x.AwardBalance
                }).ToList();
        }

        public IEnumerable<TicketModel> GetAllTickets()
        {
            return _ticketRepository
               .GetAll()
               .Select(x => new TicketModel
               {
                   Combination = x.Combination,
                   Round = x.Round,
                   Status = x.Status,
                   AwardBalance = x.AwardBalance
               }).ToList();
        }
    }
}
