

namespace Avanade.Library.BusinessLogic
{
    public interface IBusinessLogics
    {
        string RequestLogin(string Username, string Password);
        string RequestReservation(int bookId);
        string RequestAddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity);
        string RequestUpdateBook(int BookId,string Title, string AuthorName, string AuthorSurName, string PublishingHouse);
        string RequestDeleteBook(int BookId);
        string SearchBookAvailables(string title, string authorName, string authorSurName, string publishingHouse);
        string RequestReservationHistory(int BookId, int UserId, int ReservationId);
        string RequestCloseReservation(int ReservationId);
    }
}
