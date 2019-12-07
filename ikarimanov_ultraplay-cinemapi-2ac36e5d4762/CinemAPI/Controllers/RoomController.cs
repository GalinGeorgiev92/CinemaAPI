using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.RoomModels;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Input.Room;
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
        public IHttpActionResult Index(RoomCreationModel model)
        {
            NewProjectionSummary summary = newRoom.New(new Room(model.Number, model.SeatsPerRow,
                model.Rows, model.CinemaId));

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
        //public IHttpActionResult Index(RoomCreationModel model)
        //{
        //    IRoom room = roomRepo.GetByCinemaAndNumber(model.CinemaId, model.Number);

        //    if (room == null)
        //    {
        //        roomRepo.Insert(new Room(model.Number, model.SeatsPerRow, model.Rows, model.CinemaId));

        //        return Ok();
        //    }

        //    return BadRequest("Room already exists");
        //}
    }
}