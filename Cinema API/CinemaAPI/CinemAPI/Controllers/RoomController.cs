using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.RoomModels;
using CinemAPI.Models;
using CinemAPI.Models.Input.Room;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class RoomController : ApiController
    {
        private readonly INewRoom newRoom;

        public RoomController(INewRoom newRoom)
        {
            this.newRoom = newRoom;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Index(RoomCreationModel model)
        {
            NewSummary summary = await this.newRoom.New(new Room
                (model.Number, model.SeatsPerRow, model.Rows, model.CinemaId));

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