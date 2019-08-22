using Models;
using System.Collections.Generic;

namespace Business
{
    public interface ITicketService
    {
        void CreateTicket(int id,string combination);
        IEnumerable<TicketModel> GetAllByUser(int id);
        IEnumerable<TicketModel> GetAllTickets();
    }
}