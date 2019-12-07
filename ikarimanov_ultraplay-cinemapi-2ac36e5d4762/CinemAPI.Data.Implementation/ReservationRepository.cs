using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext db;
        private readonly IProjectionRepository projRepo;
        private readonly IRoomRepository roomRepo;
        private readonly IMovieRepository movieRepo;
        private readonly ICinemaRepository cinemaRepo;

        public ReservationRepository(CinemaDbContext db, IProjectionRepository projRepo, IRoomRepository roomRepo,
            IMovieRepository movieRepo, ICinemaRepository cinemaRepo)
        {
            this.db = db;
            this.projRepo = projRepo;
            this.roomRepo = roomRepo;
            this.movieRepo = movieRepo;
            this.cinemaRepo = cinemaRepo;
        }

        public IReservation GetById(int id)
        {
            var reservation = this.db.Reservations.FirstOrDefault(x => x.Id == id);

            return reservation;
        }

        public void Insert(IReservationCreation reservation)
        {
            Reservation newReservation = new Reservation(reservation.ProjectionStartDate, reservation.MovieName,
                reservation.CinemaName, reservation.RoomNumber, reservation.Row, reservation.Column, reservation.ProjectionId);

            db.Reservations.Add(newReservation);
            
            this.db.SaveChanges();
        }

        public void RemoveAllReservations(int id)
        {
            var reservations = this.db.Reservations.Where(x => x.Id == id);

            foreach (var reservation in reservations)
            {
                this.db.Reservations.Remove(reservation);
            }

            this.db.SaveChanges();
        }

        public void RemoveReservation(int id)
        {
            var reservation = this.db.Reservations.FirstOrDefault(x => x.Id == id);

            this.db.Reservations.Remove(reservation);
            this.db.SaveChanges();
        }
    }
}
