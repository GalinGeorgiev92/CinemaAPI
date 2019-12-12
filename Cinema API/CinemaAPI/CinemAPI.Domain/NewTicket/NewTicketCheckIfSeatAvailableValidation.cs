using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.TicketModels;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketCheckIfSeatAvailableValidation : INewTicket
    {
        private readonly IProjectionRepository projRepo;
        private readonly IReservationRepository reservationRepository;
        private readonly INewTicket newTicket;

        public NewTicketCheckIfSeatAvailableValidation(IProjectionRepository projRepo,
            IReservationRepository reservationRepository, INewTicket newTicket)
        {
            this.projRepo = projRepo;
            this.reservationRepository = reservationRepository;
            this.newTicket = newTicket;
        }

        public async Task<NewSummary> New(ITicketCreation ticket)
        {
            IProjection projection = await this.projRepo.GetProjectionById(ticket.ProjectionId);
            DateTime projectionStartdate = await this.projRepo.GetProjectionStartDate(projection.Id);

            if (DateTime.Now.AddMinutes(10) >= projectionStartdate)
            {
                int count = await this.reservationRepository.RemoveAllReservations(projection.Id);
                await this.projRepo.IncreaseAvailableSeats(projection.Id, count);
            }

            bool available = await this.projRepo.CheckIfSeatIsAvailable
                (ticket.ProjectionId, ticket.Row, ticket.Column);

            if (available == false)
            {
                return new NewSummary(false, StringConstants.OccupiedPlace);
            }

            return await newTicket.New(ticket);
        }
    }
}
