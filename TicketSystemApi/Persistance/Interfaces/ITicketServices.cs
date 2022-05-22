using System.Collections.Generic;
using TicketSystemApi.Models;
using TicketSystemApi.Persistance.Data;

namespace TicketSystemApi.Persistance.Interfaces
{
    public interface ITicketServices
    {
        TicketViewModel CreatTicket(TicketViewModel ticket);
        IEnumerable<Ticket> GetAllTickets();
        TicketViewModel GetTicket(string MobileNumber);
    }
}
