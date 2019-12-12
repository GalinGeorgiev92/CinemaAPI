using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionUniqueValidation : INewProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly INewProjection newProj;


        public NewProjectionUniqueValidation(IProjectionRepository projectRepo, INewProjection newProj)
        {
            this.projectRepo = projectRepo;
            this.newProj = newProj;
        }

        public async Task<NewSummary> New(IProjectionCreation proj)
        {
            IProjection projection = await this.projectRepo.Get
                (proj.MovieId, proj.RoomId, proj.StartDate, proj.AvailableSeatsCount);

            if (projection != null)
            {
                return new NewSummary(false, StringConstants.ProjectionExists);
            }

            return await newProj.New(proj);
        }
    }
}