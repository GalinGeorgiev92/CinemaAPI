﻿using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models
{
    public class Ticket : ITicket, ITicketCreation
    {
        public Ticket()
        {
        }

        public Ticket(DateTime projectionStartDate, string movieName, string cinemaName, int roomNumber, int row, int column)
        {
            this.ProjectionStartDate = projectionStartDate;
            this.MovieName = movieName;
            this.CinemaName = cinemaName;
            this.RoomNumber = roomNumber;
            this.Row = row;
            this.Column = column;
        }

        public long Id { get; set; }

        public DateTime ProjectionStartDate {get; set;}

        public string MovieName { get; set; }

        public string CinemaName { get; set; }

        public int RoomNumber { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}