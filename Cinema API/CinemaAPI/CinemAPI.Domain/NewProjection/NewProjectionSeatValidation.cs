using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionSeatValidation : INewProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly INewProjection newProj;

        public NewProjectionSeatValidation(IProjectionRepository projectRepo, INewProjection newProj)
        {
            this.projectRepo = projectRepo;
            this.newProj = newProj;
        }

        public async Task<NewSummary> New(IProjectionCreation proj)
        {
            if (proj.AvailableSeatsCount <= 0)
            {
                return new NewSummary(false, StringConstants.NegativeSeat);
            }

            return await newProj.New(proj);
        }
    }
}
