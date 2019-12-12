using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.TicketModels;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketCreation : INewTicket
    {
        private readonly IProjectionRepository projRepo;
        private readonly IMovieRepository movieRepo;
        private readonly IRoomRepository roomRepo;
        private readonly ICinemaRepository cinemaRepo;
        private readonly ITicketRepository ticketRepo;

        public NewTicketCreation(IProjectionRepository projRepo, IMovieRepository movieRepo,
                    IRoomRepository roomRepo, ICinemaRepository cinemaRepo, ITicketRepository ticketRepo)
        {
            this.projRepo = projRepo;
            this.movieRepo = movieRepo;
            this.roomRepo = roomRepo;
            this.cinemaRepo = cinemaRepo;
            this.ticketRepo = ticketRepo;
        }

        public async Task<NewSummary> New(ITicketCreation ticket)
        {
            IProjection projection = await this.projRepo.GetProjectionById(ticket.ProjectionId);
            await this.projRepo.DecreaseAvailableSeats(projection.Id);
            string movieName = await this.movieRepo.GetMovieNameById(projection.MovieId);
            var room = await this.roomRepo.GetById(projection.RoomId);
            var cinemaName = await this.cinemaRepo.GetCinemaNameById(room.CinemaId);

            await this.ticketRepo.Insert(new Ticket(projection.StartDate, movieName,
                    cinemaName, room.Number, ticket.Row, ticket.Column, projection.Id));

            return new NewSummary(true);
        }
    }
}
