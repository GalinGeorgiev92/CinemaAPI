using System;

namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicketCreation
    {
        DateTime ProjectionStartDate { get; }

        string MovieName { get; }

        string CinemaName { get; }

        int RoomNumber { get; }

        int Row { get; }

        int Column { get; }

        int ProjectionId { get; }
    }
}
