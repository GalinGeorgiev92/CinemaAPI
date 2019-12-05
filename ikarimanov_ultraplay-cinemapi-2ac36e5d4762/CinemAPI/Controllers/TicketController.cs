using CinemAPI.Data;
using CinemAPI.Models;
using CinemAPI.Models.Input.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class TicketController : ApiController
    {
        private readonly IProjectionRepository projRepo;
        private readonly IMovieRepository movieRepo;
        private readonly IRoomRepository roomRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly ICinemaRepository cinemaRepo;
        private readonly ITicketRepository ticketRepo;

        public TicketController(IProjectionRepository projRepo, IMovieRepository movieRepo,
                    IRoomRepository roomRepo, IReservationRepository reservationRepo,
                    ICinemaRepository cinemaRepo, ITicketRepository ticketRepo)
        {
            this.projRepo = projRepo;
            this.movieRepo = movieRepo;
            this.roomRepo = roomRepo;
            this.reservationRepo = reservationRepo;
            this.cinemaRepo = cinemaRepo;
            this.ticketRepo = ticketRepo;
        }

        public IHttpActionResult Index(TicketCreationModel model)
        {
            var projection = this.projRepo.GetProjectionById(model.ProjectionId);
            var movie = movieRepo.GetById(projection.MovieId);
            var room = roomRepo.GetById(projection.RoomId);
            var cinema = this.cinemaRepo.GetById(room.CinemaId);

            if (model.Row < 0 || model.Row > room.SeatsPerRow && model.Column < 0 || model.Column > room.Rows)
            {
                return BadRequest("Seat does not exist in the room");
            }

            var time = DateTime.UtcNow;

            if (time > projection.StartDate)
            {
                return BadRequest("Movie already started");
            }

            if (time.AddMinutes(10) >= projection.StartDate)
            {
                projRepo.RemoveAllReservations(model.ProjectionId);
            }

            var ticket = new Ticket(projection.StartDate, movie.Name,
                     cinema.Name, room.Number, model.Row, model.Column);

            var seat = this.projRepo.CheckIfSeatAvailable(model.ProjectionId, ticket);

            if (seat == true)
            {
                this.ticketRepo.Insert(new Ticket(projection.StartDate, movie.Name,
                    cinema.Name, room.Number, model.Row, model.Column));
                this.projRepo.DecreaseAvailableSeats(model.ProjectionId);
                this.projRepo.AddTicket(model.ProjectionId, ticket);

                return Ok("Ticket Bought!");
            }

            return BadRequest("That place is occupied!");
        }
    }
}
