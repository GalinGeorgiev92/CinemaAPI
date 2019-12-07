using CinemAPI.Data;
using CinemAPI.Domain.Contracts.CinemaModels;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using CinemAPI.Models.Input.Cinema;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class CinemaController : ApiController
    {
        private readonly INewCinema newCinema;

        public CinemaController(INewCinema newCinema)
        {
            this.newCinema = newCinema;
        }

        [HttpPost]
        public IHttpActionResult Index(CinemaCreationModel model)
        {
            NewProjectionSummary summary = newCinema.New(new Cinema(model.Name, model.Address));

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
        //public IHttpActionResult Index(CinemaCreationModel model)
        //{
        //    ICinema cinema = cinemaRepo.GetByNameAndAddress(model.Name, model.Address);

        //    if (cinema == null)
        //    {
        //        cinemaRepo.Insert(new Cinema(model.Name, model.Address));

        //        return Ok();
        //    }

        //    return BadRequest("Cinema already exists");
        //}
    }
}