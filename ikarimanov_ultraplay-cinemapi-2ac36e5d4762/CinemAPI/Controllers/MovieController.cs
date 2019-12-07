using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.MovieModels;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Input.Movie;
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
        public IHttpActionResult Index(MovieCreationModel model)
        {
            NewProjectionSummary summary = newMovie.New(new Movie(model.Name, model.DurationMinutes));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        //[HttpPost]
        //public IHttpActionResult Index(MovieCreationModel model)
        //{
        //    IMovie movie = movieRepo.GetByNameAndDuration(model.Name, model.DurationMinutes);

        //    if (movie == null)
        //    {
        //        movieRepo.Insert(new Movie(model.Name, model.DurationMinutes));

        //        return Ok();
        //    }

        //    return BadRequest("Movie already exists");
        //}
    }
}