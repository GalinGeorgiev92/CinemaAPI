using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionUniqueValidation : INewProjection, IAvailableSeatsProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly INewProjection newProj;


        public NewProjectionUniqueValidation(IProjectionRepository projectRepo, INewProjection newProj)
        {
            this.projectRepo = projectRepo;
            this.newProj = newProj;
        }

        public NewProjectionSummary New(IProjectionCreation proj)
        {
            IProjection projection = projectRepo.Get(proj.MovieId, proj.RoomId, proj.StartDate, proj.AvailableSeatsCount);

            if (proj.AvailableSeatsCount < 0)
            {
                return new NewProjectionSummary(false, "Cannot have negative seat count");
            }

            if (projection != null)
            {
                return new NewProjectionSummary(false, "Projection already exists");
            }

            return newProj.New(proj);
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