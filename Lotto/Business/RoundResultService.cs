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

        public void Draw()
        {
            RoundResult round = new RoundResult();

            List<int> winningCombination = new List<int>();
            Random random = new Random();
            int counter = 0;

            //for (int i = 0; i < winningCombination.Count; i++)
            //{
            //    int number = random.Next(1, 37);
            //    if (counter == 6)
            //        break;

            //    if(number == winningCombination[i])
            //    {
            //        i--;
            //        continue;
            //    }
            //    else
            //    {
            //        winningCombination.Add(number);
            //        counter++;
            //    }
            //}

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

            var roundId = _roundResultRepository.GetAll().Max(x => x.RoundId);

            var tickets = _ticketRepository
                .GetAll()
                .Where(x => x.Round == (roundId + 1))
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
                _roundResultRepository.Add(round);
            }
        }
    }
}
