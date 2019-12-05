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
        private readonly IAvailableSeatsProjection availableSeats;

        public ProjectionController(INewProjection newProj, IProjectionRepository projRepo,
            IAvailableSeatsProjection availableSeats)
        {
            this.newProj = newProj;
            this.projRepo = projRepo;
            this.availableSeats = availableSeats;
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

        [HttpGet()]
        public IHttpActionResult AvailableSeats(int id)
        {
            var seats = availableSeats.AvailableSeats(id);

            if (seats.IsCreated)
            {
                return Ok(seats);
            }
            else
            {
                return BadRequest(seats.Message);
            }
        }
    }
}