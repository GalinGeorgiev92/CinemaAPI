using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain
{
    public class NewProjectionCreation : INewProjection
    {
        private readonly IProjectionRepository projectionsRepo;

        public NewProjectionCreation(IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
        }

        public async Task<NewSummary> New(IProjectionCreation projection)
        {
            await this.projectionsRepo.Insert(new Projection(projection.MovieId, projection.RoomId, 
                projection.StartDate, projection.AvailableSeatsCount));

            return new NewSummary(true);
        }
    }
}