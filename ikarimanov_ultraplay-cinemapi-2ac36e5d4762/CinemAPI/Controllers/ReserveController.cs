using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Input.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ReserveController : ApiController
    {
        private readonly IProjectionRepository projRepo;
        private readonly IMovieRepository movieRepo;
        private readonly IRoomRepository roomRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly ICinemaRepository cinemaRepo;
        private readonly ITicketRepository ticketRepo;

        public ReserveController(IProjectionRepository projRepo, IMovieRepository movieRepo,
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

        [HttpPost]
        public IHttpActionResult Index(ReservationCreationModel model)
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

            if (time.AddMinutes(10) > projection.StartDate)
            {
                return BadRequest("Projection starting in less than 10 minutes.");
            }

            var reservation = new Reservation(projection.StartDate, movie.Name,
                cinema.Name, room.Number, model.Row, model.Column);

            var seat = this.projRepo.CheckReservation(model.ProjectionId, reservation);

            if (seat == true)
            {
                this.reservationRepo.Insert(new Reservation(projection.StartDate, movie.Name,
                    cinema.Name, room.Number, model.Row, model.Column));
                this.projRepo.DecreaseAvailableSeats(model.ProjectionId);
                this.projRepo.AddReservation(model.ProjectionId, reservation);

                return Ok("Reservation Created!");
            }

            return BadRequest("That place is occupied!");
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            IReservation reservation = reservationRepo.GetById(id);

            if (reservation == null)
            {
                return BadRequest("No such reservation exists");
            }

            return Ok("Reservation is cancelled");
        }

        [HttpGet]
        public IHttpActionResult BuyTicket(int id)
        {
            var reservation = this.reservationRepo.GetById(id);
            var projection = this.projRepo.GetProjectionById(id);

            var time = DateTime.UtcNow;

            if (time.AddMinutes(10) > projection.StartDate)
            {
                return BadRequest("Reservation expired (movie starting it 10 minutes).");
            }

            if(reservation == null)
            {
                return BadRequest("No such reservation exists");
            }

            var movie = movieRepo.GetById(projection.MovieId);
            var room = roomRepo.GetById(projection.RoomId);
            var cinema = this.cinemaRepo.GetById(room.CinemaId);
            Ticket ticket = new Ticket(projection.StartDate, movie.Name,
                      cinema.Name, room.Number, reservation.Row, reservation.Column);

            this.reservationRepo.RemoveReservation(id);
            this.ticketRepo.Insert(new Ticket(projection.StartDate, movie.Name,
                      cinema.Name, room.Number, reservation.Row, reservation.Column));

            var seat = this.projRepo.CheckIfSeatAvailable(id, ticket);

            if (seat == true)
            {
                this.ticketRepo.Insert(new Ticket(projection.StartDate, movie.Name,
                    cinema.Name, room.Number, ticket.Row, ticket.Column));
                this.projRepo.DecreaseAvailableSeats(id);
                this.projRepo.AddTicket(id, ticket);

                return Ok("Ticket Bought!");
            }

            return BadRequest("That place is occupied!");
        }
    }
}
