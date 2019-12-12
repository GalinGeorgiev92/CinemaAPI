using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext db;

        public ReservationRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<IReservation> GetById(int id)
        {
            Reservation reservation = await this.db.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            return reservation;
        }

        public async Task Insert(IReservationCreation reservation)
        {
            Reservation newReservation = new Reservation(reservation.ProjectionStartDate, reservation.MovieName,
                reservation.CinemaName, reservation.RoomNumber, reservation.Row, reservation.Column, reservation.ProjectionId);

            this.db.Reservations.Add(newReservation);
            
            await this.db.SaveChangesAsync();
        }

        public async Task<int> RemoveAllReservations(int id)
        {
            IQueryable<Reservation> reservations = this.db.Reservations.Where(x => x.ProjectionId == id);
            var count = reservations.Count();

            foreach (var reservation in reservations)
            {
                this.db.Reservations.Remove(reservation);
            }

            await this.db.SaveChangesAsync();

            return count;
        }

        public async Task RemoveReservation(int id)
        {
            var reservation = await this.db.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            this.db.Reservations.Remove(reservation);
            await this.db.SaveChangesAsync();
        }
    }
}
