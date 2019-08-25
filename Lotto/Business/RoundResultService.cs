using Data;
using DomainModels;
using DomainModels.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class RoundResultService : IRoundResultService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RoundResult> _roundResultRepository;


        public RoundResultService(IRepository<Ticket> ticketRepository, IRepository<User> userRepository, IRepository<RoundResult> roundResultRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _roundResultRepository = roundResultRepository;

        }

        public IEnumerable<RoundResult> GetAll()
        {
            return _roundResultRepository.GetAll();
        }

        public void Draw()
        {
            RoundResult round = new RoundResult();

            List<int> winningCombination = new List<int>();
            Random random = new Random();

            for(var i = 0; i < 7; i++)
            {
                int number = random.Next(1, 37);
                if (winningCombination.Contains(number)) {
                    i--;
                    continue;
                }

                winningCombination.Add(number);
            }

            
            round.WinningCombination = string.Join(",", winningCombination);

            int roundId = 1;

            if (_roundResultRepository.GetAll().Count() != 0)
                roundId = _roundResultRepository.GetAll().Max(x => x.Id) + 1;

            var tickets = _ticketRepository
                .GetAll()
                .Where(x => x.RoundId == roundId)
                .ToList();

            round.Tickets = tickets;

            foreach (var ticket in tickets)
            {
                var combination = ticket.Combination.Split(",").Select(x => Int32.Parse(x)).ToList();

                int combinationCounter = 0;

                foreach (var number in combination)
                {
                    if (winningCombination.Contains(number))
                        combinationCounter++;
                }

                var user = _userRepository.GetById(ticket.UserId);
                
                switch (combinationCounter)
                {
                    case 4:
                        ticket.Status = StatusEnum.Win;
                        ticket.AwardBalance = 50 * 10;
                        break;
                    case 5:
                        ticket.Status = StatusEnum.Win;
                        ticket.AwardBalance = 50 * 100;
                        break;
                    case 6:
                        ticket.Status = StatusEnum.Win;
                        ticket.AwardBalance = 50 * 1000;
                        break;
                    case 7:
                        ticket.Status = StatusEnum.Win;
                        ticket.AwardBalance = 50 * 10000;
                        break;
                    default:
                        ticket.Status = StatusEnum.Lose;
                        ticket.AwardBalance = 0;
                        break;
                }

                user.Balance = user.Balance + ticket.AwardBalance;
                _ticketRepository.Update(ticket);
                _userRepository.Update(user);
                //Puka tuka pred roundRepository
                //Da se prasha zosto vo baza tiketite taka se zacuvuvaat
                //zosto na vo TicketService ne moze da se stavi ticketot vo Tickets na Userot
                _roundResultRepository.Add(round);
            }
        }
    }
}
