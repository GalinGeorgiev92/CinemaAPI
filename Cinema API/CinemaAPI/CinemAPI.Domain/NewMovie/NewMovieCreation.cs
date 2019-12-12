using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.MovieModels;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Movie;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewMovie
{
    public class NewMovieCreation : INewMovie
    {
        private readonly IMovieRepository movieRepo;

        public NewMovieCreation(IMovieRepository movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        public async Task<NewSummary> New(IMovieCreation movie)
        {
            await this.movieRepo.Insert(new Movie(movie.Name, movie.DurationMinutes));

            return new NewSummary(true);
        }
    }
}
