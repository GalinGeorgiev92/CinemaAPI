using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.RoomModels;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Room;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewRoom
{
    public class NewRoomCreation : INewRoom
    {
        private readonly IRoomRepository roomRepo;

        public NewRoomCreation(IRoomRepository roomRepo)
        {
            this.roomRepo = roomRepo;
        }

        public async Task<NewSummary> New(IRoomCreation room)
        {
            await this.roomRepo.Insert(new Room(room.Number, room.SeatsPerRow, room.Rows, room.CinemaId));

            return new NewSummary(true);
        }
    }
}
