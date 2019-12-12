using CinemAPI.Domain.Contracts.Models;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.ReservationModel
{
    public interface IDeleteReservation
    {
        Task<NewSummary> Delete(int id);
    }
}
