using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.TicketModels;
using CinemAPI.Models;
using CinemAPI.Models.Input.Ticket;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class TicketController : ApiController
    {
        private readonly INewTicket newTicket;

        public TicketController(INewTicket newTicket)
        {
            this.newTicket = newTicket;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Index(TicketCreationModel model)
        {
            NewSummary summary = await this.newTicket.New(new Ticket
                (model.Row, model.Column, model.ProjectionId));

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
