using CinemAPI.Data;
using CinemAPI.Domain.Constants;
using CinemAPI.Domain.Contracts.CinemaModels;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Cinema;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewCinema
{
    public class NewCinemaUniqueValidation : INewCinema
    {
        private readonly ICinemaRepository cinemaRepo;
        private readonly INewCinema newCinema;

        public NewCinemaUniqueValidation(ICinemaRepository cinemaRepo, INewCinema newCinema)
        {
            this.cinemaRepo = cinemaRepo;
            this.newCinema = newCinema;
        }

        public async Task<NewSummary> New(ICinemaCreation model)
        {
            ICinema cinema = await this.cinemaRepo.GetByNameAndAddress(model.Name, model.Address);

            if (cinema != null)
            {
                return new NewSummary(false, StringConstants.CinemaExists);
            }

            return await newCinema.New(model);
        }
    }
}
