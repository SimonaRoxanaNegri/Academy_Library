using System;


namespace Avanade.Library.DAL
{
    public interface IReservationsHistory
    {
        int UserId { get; set; }
        int BookId { get; set; }
        string Title { get; set; }
        string Username { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        int ReservationId { get; set; }
        int Quantity { get; set; }
        bool Reserved { get; set; }
        string FormatterReservationHistoryString();
    }
}
