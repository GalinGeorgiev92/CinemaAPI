using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.ReservationModel;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System.Threading.Tasks;

namespace CinemAPI.Domain.DeleteReservation
{
    public class DeleteReservation : IDeleteReservation
    {
        private readonly IReservationRepository reservationRepo;
        private readonly IProjectionRepository projRepo;

        public DeleteReservation(IReservationRepository reservationRepo, IProjectionRepository projRepo)
        {
            this.reservationRepo = reservationRepo;
            this.projRepo = projRepo;
        }

        public async Task<NewSummary> Delete(int id)
        {
            IReservation reservation = await this.reservationRepo.GetById(id);
            IProjection projection = await this.projRepo.GetProjectionById(reservation.ProjectionId);
            await this.projRepo.IncreaseAvailableSeats(projection.Id, 1);
            await this.reservationRepo.RemoveReservation(id);

            return new NewSummary(true);
        }
    }
}
