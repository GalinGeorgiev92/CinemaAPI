﻿using CinemAPI.Models.Contracts.Reservation;
using System;

namespace CinemAPI.Models
{
    public class Reservation : IReservation, IReservationCreation
    {
        public Reservation()
        {
        }

        public Reservation(int row, int column, int projectionId)
        {
            this.Row = row;
            this.Column = column;
            this.ProjectionId = projectionId;
        }

        public Reservation(DateTime projectionStartDate, string movieName,
            string cinemaName, int roomNumber, int row, int column, int projectionId)
        {
            this.ProjectionStartDate = projectionStartDate;
            this.MovieName = movieName;
            this.CinemaName = cinemaName;
            this.RoomNumber = roomNumber;
            this.Row = row;
            this.Column = column;
            this.ProjectionId = projectionId;
        }

        public int Id { get; set; } 

        public DateTime ProjectionStartDate { get; set; }

        public string MovieName { get; set; }

        public string CinemaName { get; set; }

        public int RoomNumber { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public int ProjectionId { get; set; }

        public Projection Projection { get; set; }
    }
}
