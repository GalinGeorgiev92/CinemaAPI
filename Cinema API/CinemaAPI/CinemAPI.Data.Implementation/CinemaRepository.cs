using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly CinemaDbContext db;

        public CinemaRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<ICinema> GetById(int id)
        {
            return await this.db.Cinemas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICinema> GetByNameAndAddress(string name, string address)
        {
            return await this.db.Cinemas.Where(x => x.Name == name &&
                                         x.Address == address)
                                         .FirstOrDefaultAsync();
        }

        public async Task Insert(ICinemaCreation cinema)
        {
            Cinema newCinema = new Cinema(cinema.Name, cinema.Address);

            this.db.Cinemas.Add(newCinema);

            await this.db.SaveChangesAsync();
        }

        public async Task<string> GetCinemaNameById(int id)
        {
            return await this.db.Cinemas
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefaultAsync();
        }
    }
}