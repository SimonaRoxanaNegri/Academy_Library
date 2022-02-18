using System;


namespace Avanade.Library.Entities
{
    public interface IReservation
    {
        int ReservationId { get; set; }
        int UserId { get; set; }
        int BookId { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string serializableStartDate { get; set; }
        string serializableEndDate { get; set; }

    }
}
