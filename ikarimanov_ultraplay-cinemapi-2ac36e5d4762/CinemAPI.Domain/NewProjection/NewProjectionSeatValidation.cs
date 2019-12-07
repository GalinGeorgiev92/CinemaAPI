using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionSeatValidation : IAvailableSeatsProjection
    {
        private readonly IProjectionRepository projectRepo;

        public NewProjectionSeatValidation(IProjectionRepository projectRepo)
        {
            this.projectRepo = projectRepo;
        }

        public NewProjectionSummary AvailableSeats(int id)
        {
            var seats = projectRepo.AvailableSeats(id);

            if (seats < 0)
            {
                return new NewProjectionSummary(false, "Cannot have negative seat count");
            }

            if (seats == 0)
            {
                return new NewProjectionSummary(false, "There isn't a projection with that Id");
            }

            return new NewProjectionSummary(true, seats.ToString());
        }
    }
}
