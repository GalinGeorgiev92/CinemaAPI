using CinemAPI.Models.Contracts.Movie;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IMovieRepository
    {
        Task<IMovie> GetById(int movieId);

        Task<IMovie> GetByNameAndDuration(string name, short duration);

        Task Insert(IMovieCreation movie);

        Task<string> GetMovieNameById(int id);
    }
}