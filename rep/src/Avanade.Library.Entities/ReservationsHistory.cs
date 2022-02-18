using System;


namespace Avanade.Library.DAL
{
    public class ReservationsHistory : IReservationsHistory
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ReservationId { get; set; }
        public int Quantity { get; set; }
        public bool Reserved { get; set; }


        public ReservationsHistory(int userId, int bookId,string title, string username, DateTime startDate, DateTime endDate, int reservationId,int quantity, bool reserved)
        {
            UserId = userId;
            BookId = bookId;
            Title = title;
            Username = username;
            StartDate = startDate;
            EndDate = endDate;
            ReservationId = reservationId;
            Quantity = quantity;
            Reserved = reserved;
        }

        public ReservationsHistory(int bookId, string title, string username, DateTime startDate, DateTime endDate, int reservationId, bool reserved)
        {
            BookId = bookId;
            Title = title;
            Username = username;
            StartDate = startDate;
            EndDate = endDate;
            ReservationId = reservationId;
            Reserved = reserved;

        }

        public ReservationsHistory()
        {
         
        }

        public string FormatterReservationHistoryString()
        {
            return $"\nCodice libro = {this.BookId},\nCodice Prenotazione = {this.ReservationId},\nTitolo = {this.Title},\nNome Utente = {this.Username},\nData inizio prestito = {this.StartDate.ToString("dd/MM/yyyy")},\nData fine prestito = {this.EndDate.ToString("dd/MM/yyyy")},\nPrenotazione attiva = { ( this.Reserved ? "Sì" : "No" ) }";
            
        }
    }
}
