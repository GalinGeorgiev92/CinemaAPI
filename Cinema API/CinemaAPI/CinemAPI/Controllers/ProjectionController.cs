﻿using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Input.Projection;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IAvailableSeatsProjection availableSeatsProj;

        public ProjectionController(INewProjection newProj, IAvailableSeatsProjection availableSeatsProj)
        {
            this.newProj = newProj;
            this.availableSeatsProj = availableSeatsProj;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Index(ProjectionCreationModel model)
        {
            NewSummary summary = await this.newProj.New(new Projection(model.MovieId, model.RoomId, model.StartDate,
                model.AvailableSeatsCount));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> AvailableSeats(int id)
        {
            var numberOfSeats = await this.availableSeatsProj.AvailableSeats(id);

            if (numberOfSeats.IsCreated)
            {
                return Ok(numberOfSeats);
            }
            else
            {
                return BadRequest(numberOfSeats.Message);
            }
        }
    }
}