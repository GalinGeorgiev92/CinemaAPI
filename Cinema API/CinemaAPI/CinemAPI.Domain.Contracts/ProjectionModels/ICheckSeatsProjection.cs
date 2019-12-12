using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface ICheckSeatsProjection
    {
        Task<NewSummary> CheckSeat(IReservationCreation projection);
    }
}
