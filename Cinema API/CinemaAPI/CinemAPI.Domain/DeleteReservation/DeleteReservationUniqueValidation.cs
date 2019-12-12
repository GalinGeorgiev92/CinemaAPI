using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.ReservationModel;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.DeleteReservation
{
    public class DeleteReservationUniqueValidation : IDeleteReservation
    {
        private readonly IProjectionRepository projRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly IDeleteReservation deleteReservation;

        public DeleteReservationUniqueValidation(IProjectionRepository projRepo, IReservationRepository reservationRepo, IDeleteReservation deleteReservation)
        {
            this.projRepo = projRepo;
            this.reservationRepo = reservationRepo;
            this.deleteReservation = deleteReservation;
        }

        public async Task<NewSummary> Delete(int id)
        {
            IReservation reservation = await this.reservationRepo.GetById(id);

            if (reservation == null)
            {
                return new NewSummary(false, $"Reservation with id {id} does not exist");
            }

            return await deleteReservation.Delete(id);
        }
    }
}
