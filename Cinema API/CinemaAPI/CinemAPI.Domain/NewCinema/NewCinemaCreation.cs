using CinemAPI.Data;
using CinemAPI.Domain.Contracts.CinemaModels;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewCinema
{
    public class NewCinemaCreation : INewCinema
    {
        private readonly ICinemaRepository cinemaRepo;

        public NewCinemaCreation(ICinemaRepository cinemaRepo)
        {
            this.cinemaRepo = cinemaRepo;
        }

        public async Task<NewSummary> New(ICinemaCreation cinema)
        {
            await this.cinemaRepo.Insert(new Cinema(cinema.Name, cinema.Address));

            return new NewSummary(true);
        }
    }
}
