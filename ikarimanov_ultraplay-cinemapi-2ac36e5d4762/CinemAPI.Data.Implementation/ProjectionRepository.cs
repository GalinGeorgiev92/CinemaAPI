using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemAPI.Data.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly CinemaDbContext db;

        public ProjectionRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public IProjection Get(int movieId, int roomId, DateTime startDate, int availableSeatsCount)
        {
            return db.Projections.FirstOrDefault(x => x.MovieId == movieId &&
                                                      x.RoomId == roomId &&
                                                      x.StartDate == startDate &&
                                                      x.AvailableSeatsCount == availableSeatsCount);
        }

        public IEnumerable<IProjection> GetActiveProjections(int roomId)
        {
            DateTime now = DateTime.UtcNow;

            return db.Projections.Where(x => x.RoomId == roomId &&
                                             x.StartDate > now);
        }

        public void Insert(IProjectionCreation proj)
        {
            Projection newProj = new Projection(proj.MovieId, proj.RoomId, proj.StartDate, proj.AvailableSeatsCount);

            db.Projections.Add(newProj);
            db.SaveChanges();
        }

        public int AvailableSeats(int id)
        {
            DateTime now = DateTime.UtcNow;

            var seats = db.Projections.Where(x => x.Id == id && x.StartDate < now)
                .Select(x => x.AvailableSeatsCount).FirstOrDefault();

            return seats;
        }

        public IProjection GetProjectionById(long id)
        {
            return db.Projections.FirstOrDefault(x => x.Id == id);
        }

        public bool CheckReservation(int id, IReservation reservation)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            var tickets = projection.Tickets;

            foreach (var ticket in tickets)
            {
                if (ticket.Row == reservation.Row && ticket.Column == reservation.Column)
                {
                    return false;
                }
            }

            if (projection.Reservations.Count == 0 && !projection.Reservations.Contains(reservation))
            {
                return true;
            }

            return false;
        }

        public void DecreaseAvailableSeats(int id)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            projection.AvailableSeatsCount--;

            this.db.SaveChanges();
        }

        public void AddReservation(int id, IReservation reservation)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            projection.Reservations.Add(reservation);

            this.db.SaveChanges();
        }

        public void RemoveReservation(int id, IReservation reservation)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            projection.Reservations.Remove(reservation);

            this.db.SaveChanges();
        }

        public void IncreaseAvailableSeats(int id, int count)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            projection.AvailableSeatsCount += count;

            this.db.SaveChanges();
        }

        public void AddTicket(int id, ITicket ticket)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            projection.Tickets.Add(ticket);

            this.db.SaveChanges();
        }

        public bool CheckIfSeatAvailable(int id, ITicket ticket)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            var reservations = projection.Reservations;

            foreach (var reservation in reservations)
            {
                if (reservation.Row == ticket.Row && reservation.Column == ticket.Column)
                {
                    return false;
                }
            }

            if (!projection.Tickets.Contains(ticket))
            {
                return true;
            }

            return false;
        }

        public void RemoveAllReservations(int id)
        {
            var projection = this.db.Projections.FirstOrDefault(x => x.Id == id);
            var count = projection.Reservations.Count();
            IncreaseAvailableSeats((int)projection.Id, count);
            projection.Reservations.RemoveAll(x => true);

            this.db.SaveChanges();
        }
    }
}