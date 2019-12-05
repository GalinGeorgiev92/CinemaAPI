using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;

namespace CinemAPI.Models
{
    public class Projection : IProjection, IProjectionCreation
    {
        public Projection()
        {
            this.Reservations = new List<IReservation>();
            this.Tickets = new List<ITicket>();
        }

        public Projection(int movieId, int roomId, DateTime startdate, int availableSeatsCount)
            : this()
        {
            this.MovieId = movieId;
            this.RoomId = roomId;
            this.StartDate = startdate;
            this.AvailableSeatsCount = availableSeatsCount;
        }

        public long Id { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public DateTime StartDate { get; set; }

        public int AvailableSeatsCount { get; set; }

        public int ReservationId { get; set; }

        public virtual List<IReservation> Reservations { get; set; }

        public int TicketId { get; set; }

        public virtual List<ITicket> Tickets { get; set; }

    }
}