using System;

namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicket
    {
        int Id { get; }

        DateTime ProjectionStartDate { get; }

        string MovieName { get; }

        string CinemaName { get; }

        int RoomNumber { get; }

        int Row { get; }

        int Column { get; }

        int ProjectionId { get; }
    }
}
