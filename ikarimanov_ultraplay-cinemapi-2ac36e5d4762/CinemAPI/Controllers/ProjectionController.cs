using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Input.Projection;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IProjectionRepository projRepo;

        public ProjectionController(INewProjection newProj, IProjectionRepository projRepo)
        {
            this.newProj = newProj;
            this.projRepo = projRepo;
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            NewProjectionSummary summary = newProj.New(new Projection(model.MovieId, model.RoomId, model.StartDate,
                model.AvailableSeatsCount));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult AvailableSeats(int id)
        {
            var projection = projRepo.AvailableSeats(id);

            if (projection != null)
            {
                return Ok(projection.AvailableSeatsCount);
            }
            else
            {
                return BadRequest("Movie already started");
            }
        }
    }
}