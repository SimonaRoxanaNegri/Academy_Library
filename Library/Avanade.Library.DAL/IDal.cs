using Avanade.Library.Entities;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Avanade.Library.DAL
{
    public interface IDal
    {

        IUser GetUser(string Username, string Password);
        List<IBooksAvailables> GetBooksAvailables(string Title, string AuthorName, string AuthorSurName, string PublishingHouse);        
        List<IReservationsHistory> GetHistoryReservations(int BookId, int UserId, int ReservationId);
        List<IReservationsHistory> GetReservationByBookId(int bookId);
        List<IReservationsHistory> GetReservationByReservationId(int ReservationId);
        IBook GetBookById(int bookId);
//      int GetQuantityBooksById(int bookId);   
        int GetBookId(string Title, string AuthorName, string AuthorSurName, string PublishingHouse);
        int GetDuplicateBook(string title, string authorName, string authorSurName, string publishingHouse);        
        int GetReservationDays(int reservationId);
        
        int AddReservation(int userId, int bookId, DateTime startDate, DateTime endDate);        
        bool AddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity); 
        
        IBook UpdateBook(int BookId, string Title, string AuthorName, string AuthorSurName, string PublishingHouse);   
        int UpdateQuantityOfBook(int BookId, int Quantity);
        string UpdateEndDateReservation(int ReservationId);

        bool DeleteBook(int BookId);
        bool DeleteReservation( int BookId);

    }
}