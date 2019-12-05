using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionSeatValidation
    {
        private readonly ICheckSeatsProjection newProj;
        private readonly IProjectionRepository projectRepo;
        private readonly IRoomRepository roomRepo;

        public NewProjectionSeatValidation(ICheckSeatsProjection newProj, IProjectionRepository projectRepo, IRoomRepository roomRepo)
        {
            this.newProj = newProj;
            this.projectRepo = projectRepo;
            this.roomRepo = roomRepo;
        }
        //public NewProjectionSummary CheckSeat(IReservationCreation proj)
        //{
        //    bool result = projectRepo.CheckReservation(proj);

        //    if (result == false)
        //    {
        //        return new NewProjectionSummary(false, "Seat is occupied");
        //    }

        //    return new NewProjectionSummary(true, "Good job!");
        //}
    }
}
