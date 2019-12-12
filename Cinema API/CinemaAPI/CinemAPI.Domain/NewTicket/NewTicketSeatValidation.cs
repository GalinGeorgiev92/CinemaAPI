using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.TicketModels;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketSeatValidation : INewTicket
    {
        private readonly INewTicket newTicket;
        private readonly IRoomRepository roomRepo;
        private readonly IProjectionRepository projRepo;

        public NewTicketSeatValidation(INewTicket newTicket, IRoomRepository roomRepo, IProjectionRepository projRepo)
        {
            this.newTicket = newTicket;
            this.roomRepo = roomRepo;
            this.projRepo = projRepo;
        }

        public async Task<NewSummary> New(ITicketCreation ticket)
        {
            var projection = await this.projRepo.GetProjectionById(ticket.ProjectionId);
            var room = await this.roomRepo.GetById(projection.RoomId);

            if (ticket.Row < 0 || ticket.Row > room.SeatsPerRow ||
                ticket.Column < 0 || ticket.Column > room.Rows)
            {
                return new NewSummary(false, $"Seat with position row: {ticket.Row}" +
                    $" and column: {ticket.Column} does not exist");
            }

             return await newTicket.New(ticket);
        }
    }
}
