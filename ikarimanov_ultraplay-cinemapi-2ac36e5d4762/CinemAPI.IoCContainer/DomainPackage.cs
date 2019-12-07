using CinemAPI.Data.Implementation;
using CinemAPI.Domain;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.CinemaModels;
using CinemAPI.Domain.Contracts.MovieModels;
using CinemAPI.Domain.Contracts.ReservationModel;
using CinemAPI.Domain.Contracts.RoomModels;
using CinemAPI.Domain.NewCinema;
using CinemAPI.Domain.NewMovie;
using CinemAPI.Domain.NewProjection;
using CinemAPI.Domain.NewReservation;
using CinemAPI.Domain.NewRoom;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer
{
    public class DomainPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<INewProjection, NewProjectionCreation>();
            container.Register<IAvailableSeatsProjection, NewProjectionSeatValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionMovieValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionUniqueValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionRoomValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionPreviousOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionNextOverlapValidation>();


            container.Register<INewCinema, NewCinemaCreation>();
            container.RegisterDecorator<INewCinema, NewCinemaUniqueValidation>();

            container.Register<INewMovie, NewMovieCreation>();
            container.RegisterDecorator<INewMovie, NewMovieUniqueValidation>();

            container.Register<INewRoom, NewRoomCreation>();
            container.RegisterDecorator<INewRoom, NewRoomCinemaValidation>();
            container.RegisterDecorator<INewRoom, NewRoomUniqueValidation>();

            container.Register<INewReservation, NewReservationCreation>();
            container.RegisterDecorator<INewReservation, NewReservationProjectionValidation>();
            container.RegisterDecorator<INewReservation, NewReservationSeatValidation>();
            container.RegisterDecorator<INewReservation, NewReservationLateValidation>();
            container.RegisterDecorator<INewReservation, NewReservationProjectionStartingValidation>();
            container.RegisterDecorator<INewReservation, NewReservationCheckIfSeatAvailableValidation>();
        }
    }
}