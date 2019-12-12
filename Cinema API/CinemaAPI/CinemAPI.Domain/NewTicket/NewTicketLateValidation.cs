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
    public class NewTicketLateValidation : INewTicket
    {
        private readonly IProjectionRepository projRepo;
        private readonly INewTicket newTicket;

        public NewTicketLateValidation(IProjectionRepository projRepo, INewTicket newTicket)
        {
            this.projRepo = projRepo;
            this.newTicket = newTicket;
        }

        public async Task<NewSummary> New(ITicketCreation model)
        {
            IProjection projection = await this.projRepo.GetProjectionById(model.ProjectionId);

            if (DateTime.UtcNow > projection.StartDate)
            {
                return new NewSummary(false, StringConstants.MovieStarted);
            }

            return await newTicket.New(model);
        }
    }
}
