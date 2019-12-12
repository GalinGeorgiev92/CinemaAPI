using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionPreviousOverlapValidation : INewProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly IMovieRepository movieRepo;
        private readonly INewProjection newProj;

        public NewProjectionPreviousOverlapValidation(IProjectionRepository projectRepo, IMovieRepository movieRepo, INewProjection proj)
        {
            this.projectRepo = projectRepo;
            this.movieRepo = movieRepo;
            this.newProj = proj;
        }

        public async Task<NewSummary> New(IProjectionCreation proj)
        {
            IEnumerable<IProjection> movieProjectionsInRoom = this.projectRepo.GetActiveProjections(proj.RoomId);

            IProjection previousProjection = movieProjectionsInRoom.Where(x => x.StartDate < proj.StartDate)
                                                                        .OrderByDescending(x => x.StartDate)
                                                                        .FirstOrDefault();
            if (previousProjection != null)
            {
                IMovie previousProjectionMovie = await this.movieRepo.GetById(previousProjection.MovieId);

                DateTime previousProjectionEnd = previousProjection.StartDate
                    .AddMinutes(previousProjectionMovie.DurationMinutes);

                if (previousProjectionEnd >= proj.StartDate)
                {
                    return new NewSummary(false, $"Projection overlaps with previous one:" +
                    $" {previousProjectionMovie.Name} at {previousProjection.StartDate}");
                }
            }

            return await newProj.New(proj);
        }
    }
}