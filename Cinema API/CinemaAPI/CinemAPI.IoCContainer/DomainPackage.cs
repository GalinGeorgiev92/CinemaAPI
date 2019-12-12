using CinemAPI.Domain;
using CinemAPI.Domain.AvailableSeatProjection;
using CinemAPI.Domain.BuyTicketReservation;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.CinemaModels;
using CinemAPI.Domain.Contracts.MovieModels;
using CinemAPI.Domain.Contracts.ReservationModel;
using CinemAPI.Domain.Contracts.RoomModels;
using CinemAPI.Domain.Contracts.TicketModels;
using CinemAPI.Domain.DeleteReservation;
using CinemAPI.Domain.NewCinema;
using CinemAPI.Domain.NewMovie;
using CinemAPI.Domain.NewProjection;
using CinemAPI.Domain.NewReservation;
using CinemAPI.Domain.NewRoom;
using CinemAPI.Domain.NewTicket;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer
{
    public class DomainPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<INewProjection, NewProjectionCreation>();
            container.RegisterDecorator<INewProjection, NewProjectionPreviousOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionNextOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionMovieValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionRoomValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionSeatValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionUniqueValidation>();
            
            container.Register<IAvailableSeatsProjection, AvailableSeatProjectionStartedValidation>();
            container.RegisterDecorator<IAvailableSeatsProjection, AvailableSeatProjectionValidation>();

            container.Register<INewCinema, NewCinemaCreation>();
            container.RegisterDecorator<INewCinema, NewCinemaUniqueValidation>();

            container.Register<INewMovie, NewMovieCreation>();
            container.RegisterDecorator<INewMovie, NewMovieUniqueValidation>();

            container.Register<INewRoom, NewRoomCreation>();
            container.RegisterDecorator<INewRoom, NewRoomCinemaValidation>();
            container.RegisterDecorator<INewRoom, NewRoomUniqueValidation>();

            container.Register<INewReservation, NewReservationCreation>();
            container.RegisterDecorator<INewReservation, NewReservationCheckIfSeatAvailableValidation>();
            container.RegisterDecorator<INewReservation, NewReservationProjectionStartingValidation>();
            container.RegisterDecorator<INewReservation, NewReservationLateValidation>();
            container.RegisterDecorator<INewReservation, NewReservationSeatValidation>();
            container.RegisterDecorator<INewReservation, NewReservationProjectionValidation>();

            container.Register<IDeleteReservation, DeleteReservation>();
            container.RegisterDecorator<IDeleteReservation, DeleteReservationUniqueValidation>();

            container.Register<INewTicket, NewTicketCreation>();
            container.RegisterDecorator<INewTicket, NewTicketCheckIfSeatAvailableValidation>();
            container.RegisterDecorator<INewTicket, NewTicketLateValidation>();
            container.RegisterDecorator<INewTicket, NewTicketSeatValidation>();
            container.RegisterDecorator<INewTicket, NewTicketCheckIfAvailableProjection>();

            container.Register<IBuyTicket, BuyTicketCreation>();
            container.RegisterDecorator<IBuyTicket, BuyTicketReservationExpiredValidation>();
            container.RegisterDecorator<IBuyTicket, BuyTicketLateValidation>();
            container.RegisterDecorator<IBuyTicket, BuyTicketReservationValidation>();
        }
    }
}