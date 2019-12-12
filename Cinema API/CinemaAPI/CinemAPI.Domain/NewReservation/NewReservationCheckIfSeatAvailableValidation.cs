using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.ReservationModel;
using CinemAPI.Models.Contracts.Reservation;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationCheckIfSeatAvailableValidation : INewReservation
    {
        private readonly IProjectionRepository projRepo;
        private readonly INewReservation newReservation;

        public NewReservationCheckIfSeatAvailableValidation(IProjectionRepository projRepo, INewReservation newReservation)
        {
            this.projRepo = projRepo;
            this.newReservation = newReservation;
        }

        public async Task<NewSummary> New(IReservationCreation reservation)
        {
            bool available = await this.projRepo.CheckIfSeatIsAvailable
                (reservation.ProjectionId, reservation.Row, reservation.Column);

            if (available == false)
            {
                return new NewSummary(false, StringConstants.OccupiedPlace);
            }

            return await newReservation.New(reservation);
        }
    }
}
