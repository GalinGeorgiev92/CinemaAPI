using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.MovieModels;
using CinemAPI.Models.Contracts.Movie;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewMovie
{
    public class NewMovieUniqueValidation : INewMovie
    {
        private readonly IMovieRepository movieRepo;
        private readonly INewMovie newMovie;

        public NewMovieUniqueValidation(IMovieRepository movieRepo, INewMovie newMovie)
        {
            this.movieRepo = movieRepo;
            this.newMovie = newMovie;
        }

        public async Task<NewSummary> New(IMovieCreation model)
        {
            IMovie movie = await this.movieRepo.GetByNameAndDuration(model.Name, model.DurationMinutes);

            if (movie != null)
            {
                return new NewSummary(false, StringConstants.MovieExists);
            }

            return await newMovie.New(model);
        }
    }
}
