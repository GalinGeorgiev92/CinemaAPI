using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Movie;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.MovieModels
{
    public interface INewMovie
    {
        Task<NewSummary> New(IMovieCreation movie);
    }
}
