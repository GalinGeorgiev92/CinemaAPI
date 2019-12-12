using CinemAPI.Models.Contracts.Room;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IRoomRepository
    {
        Task<ICinema> GetById(int id);

        Task<ICinema> GetByCinemaAndNumber(int cinemaId, int number);

        Task Insert(IRoomCreation room);
    }
}