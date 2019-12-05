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
        void Insert(IReservationCreation reservation);

        IReservation GetById(int id);

        void RemoveReservation(int id);
    }
}
