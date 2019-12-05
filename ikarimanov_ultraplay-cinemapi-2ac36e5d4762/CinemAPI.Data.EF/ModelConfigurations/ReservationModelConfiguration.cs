using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    internal sealed class ReservationModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Reservation> cinemaModel = modelBuilder.Entity<Reservation>();
            cinemaModel.HasKey(model => model.Id);
            cinemaModel.Property(model => model.ProjectionStartDate).IsRequired();
            cinemaModel.Property(model => model.MovieName).IsRequired();
            cinemaModel.Property(model => model.CinemaName).IsRequired();
            cinemaModel.Property(model => model.RoomNumber).IsRequired();
            cinemaModel.Property(model => model.Row).IsRequired();
            cinemaModel.Property(model => model.Column).IsRequired();
        }
    }
}
