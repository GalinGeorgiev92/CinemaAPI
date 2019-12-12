using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.ReservationModel;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.BuyTicketReservation
{
    public class BuyTicketLateValidation : IBuyTicket
    {
        private readonly IProjectionRepository projRepo;
        private readonly IBuyTicket buyTicket;
        private readonly IReservationRepository reservationRepository;

        public BuyTicketLateValidation(IProjectionRepository projRepo, IBuyTicket buyTicket, IReservationRepository reservationRepository)
        {
            this.projRepo = projRepo;
            this.buyTicket = buyTicket;
            this.reservationRepository = reservationRepository;
        }

        public async Task<NewSummary> Buy(int id)
        {
            IReservation reservation = await this.reservationRepository.GetById(id);
            DateTime projectionStartdate = await this.projRepo.GetProjectionStartDate(reservation.ProjectionId);

            if (DateTime.Now > projectionStartdate)
            {
                return new NewSummary(false, StringConstants.MovieStarted);
            }

            return await buyTicket.Buy(id);
        }
    }
}
