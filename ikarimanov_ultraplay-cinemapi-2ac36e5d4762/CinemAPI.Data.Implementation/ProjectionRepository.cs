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

            var seats = db.Projections.Where(x => x.Id == id && x.StartDate > now)
                .Select(x => x.AvailableSeatsCount).FirstOrDefault();

            return seats;
        }

        public IProjection GetProjectionById(int id)
        {
            return db.Projections.FirstOrDefault(x => x.Id == id);
        }

        public bool CheckIfSeatIsAvailable(int id, int row, int col)
        {
            var reservations = db.Reservations.Where(x => x.ProjectionId == id);

            foreach (var reservation in reservations)
            {
                if(reservation.Row == row && reservation.Column == col)
                {
                    return false;
                }
            }

            var tickets = db.Tickets.Where(x => x.ProjectionId == id);

            foreach (var ticket in tickets)
            {
                if (ticket.Row == row && ticket.Column == col)
                {
                    return false;
                }
            }

            return true;
        }

        public void DecreaseAvailableSeats(int id)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            projection.AvailableSeatsCount--;

            this.db.SaveChanges();
        }

        public void IncreaseAvailableSeats(int id, int count)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            projection.AvailableSeatsCount += count;

            this.db.SaveChanges();
        }

        public void AddTicket(int id, Ticket ticket)
        {
            var projection = db.Projections.FirstOrDefault(x => x.Id == id);
            projection.Tickets.Add(ticket);

            this.db.SaveChanges();
        }
    }
}