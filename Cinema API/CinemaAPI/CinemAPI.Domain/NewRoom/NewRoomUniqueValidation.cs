using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.RoomModels;
using CinemAPI.Models.Contracts.Room;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewRoom
{
    public class NewRoomUniqueValidation : INewRoom
    {
        private readonly IRoomRepository roomRepo;
        private readonly INewRoom newRoom;

        public NewRoomUniqueValidation(IRoomRepository roomRepo, INewRoom newRoom)
        {
            this.roomRepo = roomRepo;
            this.newRoom = newRoom;
        }

        public async Task<NewSummary> New(IRoomCreation model)
        {
            ICinema room = await this.roomRepo.GetByCinemaAndNumber(model.CinemaId, model.Number);

            if (room != null)
            {
                return new NewSummary(false, StringConstants.RoomExists);
            }

            return await newRoom.New(model);
        }
    }
}
