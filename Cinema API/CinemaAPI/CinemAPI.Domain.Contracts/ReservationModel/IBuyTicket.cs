using CinemAPI.Domain.Contracts.Models;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.ReservationModel
{
    public interface IBuyTicket
    {
        Task<NewSummary> Buy(int id);
    }
}
