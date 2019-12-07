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
            if (projection == null)
            {
                return BadRequest("No such projection exists");
            }

            var movie = this.movieRepo.GetById(projection.MovieId);
            var room = this.roomRepo.GetById(projection.RoomId);
            var cinema = this.cinemaRepo.GetById(room.CinemaId);

            if (model.Row < 0 || model.Row > room.SeatsPerRow || model.Column < 0 || model.Column > room.Rows)
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
                reservationRepo.RemoveAllReservations(model.ProjectionId);
            }

            var available = this.projRepo.CheckIfSeatIsAvailable(model.ProjectionId, model.Row, model.Column);

            if (available == true)
            {
                this.ticketRepo.Insert(new Ticket(projection.StartDate, movie.Name,
                    cinema.Name, room.Number, model.Row, model.Column, projection.Id));
                this.projRepo.DecreaseAvailableSeats(model.ProjectionId);

                return Ok("Ticket Bought!");
            }

            return BadRequest("That place is occupied!");
        }
    }
}
