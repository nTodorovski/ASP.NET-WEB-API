using Models;
using System.Collections.Generic;

namespace Business
{
    public interface ITicketService
    {
        void CreateTicket(TicketModel model);
        IEnumerable<TicketModel> GetAllByUser(int id);
        IEnumerable<TicketModel> GetAllTickets();
    }
}