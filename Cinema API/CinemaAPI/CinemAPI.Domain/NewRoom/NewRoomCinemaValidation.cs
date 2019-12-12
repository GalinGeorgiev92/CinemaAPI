using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.RoomModels;
using CinemAPI.Models.Contracts.Room;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewRoom
{
    public class NewRoomCinemaValidation : INewRoom
    {
        private readonly ICinemaRepository cinemaRepo;
        private readonly INewRoom newRoom;

        public NewRoomCinemaValidation(ICinemaRepository cinemaRepo, INewRoom newRoom)
        {
            this.cinemaRepo = cinemaRepo;
            this.newRoom = newRoom;
        }

        public async Task<NewSummary> New(IRoomCreation model)
        {
            var cinema = await this.cinemaRepo.GetById(model.CinemaId);

            if (cinema == null)
            {
                return new NewSummary(false, $"Cinema with id {model.CinemaId} does not exist");
            }

            return await newRoom.New(model);
        }
    }
}
