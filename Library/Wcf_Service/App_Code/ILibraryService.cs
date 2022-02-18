using Avanade.Library.DAL;
using Avanade.Library.Entities;
using System.Collections.Generic;
using System.ServiceModel;



    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface ILibraryService
    {

        [OperationContract]
        IUser RequestLogin(string Username, string Password);
        [OperationContract]
        IResponse<IReservationsHistory> RequestReservation(IReservation Reservation);
        [OperationContract]
        IResponse<IBook> RequestAddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity);
        [OperationContract]
        IResponse<IBook> RequestUpdateBook(int BookId, string Title, string AuthorName, string AuthorSurName, string PublishingHouse);
        [OperationContract]
        IResponse<IBook> RequestDeleteBook(int BookId);
        [OperationContract]
         List<IBooksAvailables> SearchBookAvailables(string title, string authorName, string authorSurName, string publishingHouse);
        [OperationContract]
        List<IReservationsHistory> RequestReservationHistory(int BookId, int UserId, int ReservationId);
        [OperationContract]
        IResponse<IReservationsHistory> RequestCloseReservation(int ReservationId);

    }







