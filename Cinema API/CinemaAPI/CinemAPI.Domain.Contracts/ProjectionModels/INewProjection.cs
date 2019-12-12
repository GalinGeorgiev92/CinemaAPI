using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface INewProjection
    {
        Task<NewSummary> New(IProjectionCreation projection);
    }
}