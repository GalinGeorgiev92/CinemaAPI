using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.ReservationModel
{
    public interface INewReservation
    {
        Task<NewSummary> New(IReservationCreation reservation);
    }
}
