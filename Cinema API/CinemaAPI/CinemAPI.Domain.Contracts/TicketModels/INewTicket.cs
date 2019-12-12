using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.TicketModels
{
    public interface INewTicket
    {
        Task<NewSummary> New(ITicketCreation ticket);
    }
}
