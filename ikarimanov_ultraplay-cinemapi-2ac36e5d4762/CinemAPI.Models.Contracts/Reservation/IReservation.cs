﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservation
    {
        long Id { get; }

        DateTime ProjectionStartDate { get; }

        string MovieName { get; }

        string CinemaName { get; }

        int RoomNumber { get; }

        int Row { get; }

        int Column { get; }
    }
}