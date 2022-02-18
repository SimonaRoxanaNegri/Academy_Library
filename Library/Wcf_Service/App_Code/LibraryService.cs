
using System.Collections.Generic;
using Avanade.Library.BusinessLogic;
using Avanade.Library.DAL;
using Avanade.Library.Entities;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class LibraryService : ILibraryService
    {

        readonly IBusinessLogics logic = new BusinessLogics(new DbDal());


        public IResponse<IBook> RequestAddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity)
        {
            return logic.RequestAddBook(Title, AuthorName, AuthorSurName, PublishingHouse, Quantity);
        }

        public IResponse<IReservationsHistory> RequestCloseReservation(int ReservationId)
        {
            return logic.RequestCloseReservation(ReservationId);
        }

        public IResponse<IBook> RequestDeleteBook(int BookId)
        {
            return logic.RequestDeleteBook(BookId);
        }

        public IUser RequestLogin(string Username, string Password)
        {
            return logic.RequestLogin(Username, Password);
        }

        public IResponse<IReservationsHistory> RequestReservation(IReservation Reservation)
        {
            return logic.RequestReservation(Reservation);
        }

        public List<IReservationsHistory> RequestReservationHistory(int BookId, int UserId, int ReservationId)
        {
            return logic.RequestReservationHistory(BookId, UserId, ReservationId);
        }

        public IResponse<IBook> RequestUpdateBook(int BookId, string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {
            return logic.RequestUpdateBook(BookId, Title, AuthorName, AuthorSurName, PublishingHouse);
        }

        public List<IBooksAvailables> SearchBookAvailables(string title, string authorName, string authorSurName, string publishingHouse)
        {
            return logic.SearchBookAvailables(title, authorName, authorSurName, publishingHouse);
        }
    }






