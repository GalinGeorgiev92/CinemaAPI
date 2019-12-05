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

        IProjection GetProjectionById(long id);

        bool CheckReservation(int id, IReservation reservation);

        bool CheckIfSeatAvailable(int id, ITicket ticket);

        void DecreaseAvailableSeats(int id);

        void IncreaseAvailableSeats(int id, int count);

        void AddReservation(int id, IReservation reservation);

        void RemoveReservation(int id, IReservation reservation);

        void AddTicket(int id, ITicket ticket);

        void RemoveAllReservations(int id);
    }
}