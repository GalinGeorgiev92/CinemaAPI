using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        Task Insert(IReservationCreation reservation);

        Task<IReservation> GetById(int id);

        Task RemoveReservation(int id);

        Task<int> RemoveAllReservations(int id);
    }
}
