using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        Task<IProjection> Get(int movieId, int roomId, DateTime startDate, int AvailableSeatsCount);

        Task Insert(IProjectionCreation projection);

        IQueryable<IProjection> GetActiveProjections(int roomId);

        Task<int> AvailableSeats(int id);

        Task<IProjection> GetProjectionById(int id);

        Task<bool> CheckIfSeatIsAvailable(int id, int row, int col);

        Task DecreaseAvailableSeats(int id);

        Task IncreaseAvailableSeats(int id, int count);

        Task<DateTime> GetProjectionStartDate(int id);

    }
}