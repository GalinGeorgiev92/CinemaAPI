using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly CinemaDbContext db;

        public ProjectionRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<IProjection> Get(int movieId, int roomId, DateTime startDate, int availableSeatsCount)
        {
            return await this.db.Projections.FirstOrDefaultAsync(x => x.MovieId == movieId &&
                                                      x.RoomId == roomId &&
                                                      x.StartDate == startDate &&
                                                      x.AvailableSeatsCount == availableSeatsCount);
        }

        public IQueryable<IProjection> GetActiveProjections(int roomId)
        {
            DateTime now = DateTime.UtcNow;

            return db.Projections.Where(x => x.RoomId == roomId &&
                                             x.StartDate > now);
        }

        public async Task Insert(IProjectionCreation proj)
        {
            Projection newProj = new Projection(proj.MovieId, proj.RoomId, proj.StartDate, proj.AvailableSeatsCount);

            this.db.Projections.Add(newProj);
            await this.db.SaveChangesAsync();
        }

        public async Task<int> AvailableSeats(int id)
        {
            DateTime now = DateTime.Now;

            var seats = await this.db.Projections.Where(x => x.Id == id && x.StartDate > now)
                .Select(x => x.AvailableSeatsCount).FirstOrDefaultAsync();

            return seats;
        }

        public async Task<IProjection> GetProjectionById(int id)
        {
            return await this.db.Projections.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CheckIfSeatIsAvailable(int id, int row, int col)
        {
            IQueryable<IReservation> reservations = this.db.Reservations.Where(x => x.ProjectionId == id);

            foreach (var reservation in reservations)
            {
                if(reservation.Row == row && reservation.Column == col)
                {
                    return false;
                }
            }

            IQueryable<ITicket> tickets = this.db.Tickets.Where(x => x.ProjectionId == id);

            foreach (var ticket in tickets)
            {
                if (ticket.Row == row && ticket.Column == col)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task DecreaseAvailableSeats(int id)
        {
            var projection = await this.db.Projections.FirstOrDefaultAsync(x => x.Id == id);
            projection.AvailableSeatsCount--;

            await this.db.SaveChangesAsync();
        }

        public async Task IncreaseAvailableSeats(int id, int count)
        {
            var projection = await this.db.Projections.FirstOrDefaultAsync(x => x.Id == id);
            projection.AvailableSeatsCount += count;

            await this.db.SaveChangesAsync();
        }

        public async Task<DateTime> GetProjectionStartDate(int id)
        {
            return await this.db.Projections
                .Where(x => x.Id == id)
                .Select(x => x.StartDate)
                .FirstOrDefaultAsync();
        }
    }
}