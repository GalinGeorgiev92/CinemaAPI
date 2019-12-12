namespace CinemAPI.Models.Input.Reservation
{
    public class ReservationCreationModel
    {
        public int ProjectionId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}