namespace CinemAPI.Models.Contracts.Room
{
    public interface ICinema
    {
       int Id { get; }

       int CinemaId { get; }

       int Number { get; }

       short SeatsPerRow { get; }

       short Rows { get; }
    }
}