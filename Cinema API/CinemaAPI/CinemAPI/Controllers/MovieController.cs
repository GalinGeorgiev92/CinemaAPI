using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.MovieModels;
using CinemAPI.Models;
using CinemAPI.Models.Input.Movie;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class MovieController : ApiController
    {
        private readonly INewMovie newMovie;

        public MovieController(INewMovie newMovie)
        {
            this.newMovie = newMovie;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Index(MovieCreationModel model)
        {
            NewSummary summary = await this.newMovie.New(new Movie(model.Name, model.DurationMinutes));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}