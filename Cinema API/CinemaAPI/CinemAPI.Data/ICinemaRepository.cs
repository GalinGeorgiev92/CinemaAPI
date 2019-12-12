using CinemAPI.Models.Contracts.Cinema;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface ICinemaRepository
    {
        Task<ICinema> GetByNameAndAddress(string name, string address);

        Task<ICinema> GetById(int id);

        Task Insert(ICinemaCreation cinema);

        Task<string> GetCinemaNameById(int id);
    }
}