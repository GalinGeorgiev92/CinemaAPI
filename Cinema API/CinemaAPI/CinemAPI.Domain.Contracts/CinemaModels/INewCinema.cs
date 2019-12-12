using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Cinema;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.CinemaModels
{
    public interface INewCinema
    {
        Task<NewSummary> New(ICinemaCreation cinema);
    }
}
