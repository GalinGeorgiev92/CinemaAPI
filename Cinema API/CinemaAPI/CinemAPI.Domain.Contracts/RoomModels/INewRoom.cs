using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Room;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.RoomModels
{
    public interface INewRoom
    {
        Task<NewSummary> New(IRoomCreation room);
    }
}
