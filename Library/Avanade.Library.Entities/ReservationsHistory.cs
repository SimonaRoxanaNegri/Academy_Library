using System;
using System.Runtime.Serialization;

namespace Avanade.Library.DAL
{
    [DataContract]
    public class ReservationsHistory : IReservationsHistory
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int BookId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public int ReservationId { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
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
