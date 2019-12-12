using CinemAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaAPI.Tests
{
    [TestClass]
    public class CinemaRepositoryTests
    {
        private List<Cinema> GetTestCinemas()
        {
            var testCinemas = new List<Cinema>();
            testCinemas.Add(new Cinema { Id = 1, Name = "Demo1", Address = "Address1" });
            testCinemas.Add(new Cinema { Id = 2, Name = "Demo2", Address = "Address2" });
            testCinemas.Add(new Cinema { Id = 3, Name = "Demo3", Address = "Address3" });
            testCinemas.Add(new Cinema { Id = 4, Name = "Demo4", Address = "Address4" });

            return testCinemas;
        }

        [TestMethod]
        public async Task GetById_ShouldReturnCinema()
        {

        }
    }
}
