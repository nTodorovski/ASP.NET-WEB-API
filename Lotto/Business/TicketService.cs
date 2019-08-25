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

        public void CreateTicket(TicketModel model)
        {
            var user = _userRepository.GetById(model.UserId);
            if (user == null)
                throw new Exception("User not found!");

            if (user.Balance <= 50)
                throw new Exception("You don't have enough money to place a ticket!");

            int nextRound = 1;

            if(_roundResultRepository.GetAll().Count() != 0)
                nextRound = _roundResultRepository.GetAll().Max(x => x.Id) + 1;

            Regex regex = new Regex(@"^(\d{1}|\d{2})[,](\d{1}|\d{2})[,](\d{1}|\d{2})[,](\d{1}|\d{2})[,](\d{1}|\d{2})[,](\d{1}|\d{2})[,](\d{1}|\d{2})$");
            var match = regex.Match(model.Combination);
            if (!match.Success)
                throw new Exception("Invalid combination.Please enter 7 numbers seperated by commas.");

            var numbers = model.Combination.Split(",").Select(x => int.Parse(x)).ToList();

            foreach (var number in numbers)
            {
                if (number < 1 || number > 37)
                    throw new Exception("Enter valid numbers from 1 to 37!");
            }
            var duplicates = numbers
                .GroupBy(s => s)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);

            if (duplicates.Count() > 0)
                throw new Exception("You have duplicate numbers!");

            var ticket = new Ticket()
            {
                Combination = model.Combination,
                UserId = model.UserId,
                Status = StatusEnum.Pending,
                AwardBalance = 0,
                RoundId = nextRound
            };

            user.Balance = user.Balance - 50;
            //TODO: Da se prasha za ova zosto ne raboti
            //user.Tickets.Add(ticket);
            _userRepository.Update(user);
            _ticketRepository.Add(ticket);
        }

        public IEnumerable<TicketModel> GetAllByUser(int id)
        {
            return _ticketRepository
                .GetAll()
                .Where(x => x.UserId == id)
                .Select(x => new TicketModel
                {
                    UserId = id,
                    Combination = x.Combination
                }).ToList();
        }

        public IEnumerable<TicketModel> GetAllTickets()
        {
            return _ticketRepository
               .GetAll()
               .Select(x => new TicketModel
               {
                   Combination = x.Combination
               }).ToList();
        }
    }
}
