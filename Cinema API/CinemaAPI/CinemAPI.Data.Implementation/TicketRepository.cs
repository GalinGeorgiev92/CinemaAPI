using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;

        public TicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(ticket.ProjectionStartDate, ticket.MovieName,
                ticket.CinemaName, ticket.RoomNumber, ticket.Row, ticket.Column, ticket.ProjectionId);

            this.db.Tickets.Add(newTicket);

            await this.db.SaveChangesAsync();
        }
    }
}
