
using Avanade.Library.DAL;
using Avanade.Library.Entities;
using System.Collections.Generic;

namespace Avanade.Library.BusinessLogic
{
    public interface IBusinessLogics
    {
        IUser RequestLogin(string Username, string Password);
        IResponse<IReservationsHistory> RequestReservation(IReservation Reservation);
        IResponse<IBook> RequestAddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity);
        IResponse<IBook> RequestUpdateBook(int BookId, string Title, string AuthorName, string AuthorSurName, string PublishingHouse);
        IResponse<IBook> RequestDeleteBook(int BookId);
        List<IBooksAvailables> SearchBookAvailables(string title, string authorName, string authorSurName, string publishingHouse);
        List<IReservationsHistory> RequestReservationHistory(int BookId, int UserId, int ReservationId);
        IResponse<IReservationsHistory> RequestCloseReservation(int ReservationId);
    }
}
