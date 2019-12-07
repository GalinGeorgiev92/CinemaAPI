using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models
{
    public class Projection : IProjection, IProjectionCreation
    {
        private int availableSeatsCount;

        public Projection()
        {
            this.Reservations = new List<Reservation>();
            this.Tickets = new List<Ticket>();
        }

        public Projection(int movieId, int roomId, DateTime startdate, int availableSeatsCount)
            : this()
        {
            this.MovieId = movieId;
            this.RoomId = roomId;
            this.StartDate = startdate;
            this.AvailableSeatsCount = availableSeatsCount;
        }

        public int Id { get; set; }

        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public DateTime StartDate { get; set; }

        //[Range(0, int.MaxValue)]
        public int AvailableSeatsCount
        {
            get; set;
            //get => this.availableSeatsCount;
            //set
            //{
            //    if (this.availableSeatsCount < 0)
            //    {
            //        throw new ArgumentOutOfRangeException("Cannot have negative available seats count.");
            //    }

            //    this.availableSeatsCount = value;
            //}
        }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}