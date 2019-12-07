using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        IProjection Get(int movieId, int roomId, DateTime startDate, int AvailableSeatsCount);

        void Insert(IProjectionCreation projection);

        IEnumerable<IProjection> GetActiveProjections(int roomId);

        int AvailableSeats(int id);

        IProjection GetProjectionById(int id);

        bool CheckIfSeatIsAvailable(int id, int row, int col);

        void DecreaseAvailableSeats(int id);

        void IncreaseAvailableSeats(int id, int count);

        void AddTicket(int id, Ticket ticket);


    }
}