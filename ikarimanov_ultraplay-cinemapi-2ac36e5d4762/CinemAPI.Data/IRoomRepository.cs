using CinemAPI.Models.Contracts.Room;

namespace CinemAPI.Data
{
    public interface IRoomRepository
    {
        ICinema GetById(int id);

        ICinema GetByCinemaAndNumber(int cinemaId, int number);

        void Insert(IRoomCreation room);
    }
}