using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionReservationCreating : IReserveSeatsProjection
    {
        public NewProjectionSummary New(int id, int row, int col)
        {
            throw new NotImplementedException();
        }

        public NewProjectionSummary New(IReservationCreation projection)
        {
            throw new NotImplementedException();
        }
    }
}
