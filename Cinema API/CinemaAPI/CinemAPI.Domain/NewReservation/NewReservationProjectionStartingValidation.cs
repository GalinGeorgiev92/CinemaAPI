using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.ReservationModel;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationProjectionStartingValidation : INewReservation
    {
        private readonly IProjectionRepository projRepo;
        private readonly INewReservation newReservation;

        public NewReservationProjectionStartingValidation(IProjectionRepository projRepo, INewReservation newReservation)
        {
            this.projRepo = projRepo;
            this.newReservation = newReservation;
        }

        public async Task<NewSummary> New(IReservationCreation model)
        {
            DateTime projectionStartDate = await this.projRepo.GetProjectionStartDate(model.ProjectionId);

            if (DateTime.Now.AddMinutes(10) > projectionStartDate)
            {
                return new NewSummary(false, StringConstants.ProjectionIsStarting);
            }

            return await newReservation.New(model);
        }
    }
}
