using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;
        private readonly IProjectionRepository projRepo;

        public TicketRepository(CinemaDbContext db, IProjectionRepository projRepo)
        {
            this.db = db;
            this.projRepo = projRepo;
        }

        public ITicket GetById(int id)
        {
            return this.db.Tickets.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(ticket.ProjectionStartDate, ticket.MovieName,
                ticket.CinemaName, ticket.RoomNumber, ticket.Row, ticket.Column, ticket.ProjectionId);

            db.Tickets.Add(newTicket);

            this.db.SaveChanges();
        }
    }
}
