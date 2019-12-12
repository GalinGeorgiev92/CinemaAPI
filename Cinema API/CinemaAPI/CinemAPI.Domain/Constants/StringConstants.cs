using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Constants
{
    public class StringConstants
    {
        public const string ProjectionAlreadyStarted = "Projection already started or finished";
        public const string MovieStarted = "Movie already started";
        public const string ReservationExpired = "Reservation Expired";
        public const string ReservationDoesNotExist = "Reservation does not exist";
        public const string CinemaExists = "Cinema already exists";
        public const string MovieExists = "Movie already exists";
        public const string RoomExists = "Room already exists";
        public const string ProjectionExists = "Projection already exists";
        public const string NegativeSeat = "Cannot have negative seat count";
        public const string OccupiedPlace = "That place is occupied";
        public const string ProjectionIsStarting = "Projection starting in less than 10 minutes";
    }
}
