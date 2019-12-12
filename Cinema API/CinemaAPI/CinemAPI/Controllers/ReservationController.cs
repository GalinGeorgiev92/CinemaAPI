using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.ReservationModel;
using CinemAPI.Models;
using CinemAPI.Models.Input.Reservation;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly INewReservation newReservation;
        private readonly IDeleteReservation deleteReservation;
        private readonly IBuyTicket buyTicket;

        public ReservationController(INewReservation newReservation,IDeleteReservation deleteReservation,
            IBuyTicket buyTicket)
        {
            this.newReservation = newReservation;
            this.deleteReservation = deleteReservation;
            this.buyTicket = buyTicket;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Index(ReservationCreationModel model)
        {
            NewSummary summary = await this.newReservation.New
                (new Reservation(model.Row, model.Column, model.ProjectionId));

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
        public async Task<IHttpActionResult> BuyTicket(int id)
        {
            NewSummary summary = await this.buyTicket.Buy(id);

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Cancel(int id)
        {

            NewSummary summary = await this.deleteReservation.Delete(id);

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
