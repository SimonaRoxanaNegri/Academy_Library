

namespace Avanade.Library.ConsoleApp
{
    public interface IViewHandler
    {
        void AdminLogin();
        void UserLogin(string Role);
        void CloseApplication();
        void AddBookResult(string Result);
        void PrintBookId();
        void BookDeleteResult(string resultBookDelete);
        void GreetingsMessage();
        void AskUserName();
        void AskPassword();
        void PrintSearchFilters();
        void PrintBookTitle();
        void PrintBookAuthorName();
        void PrintBookAuthorSurName();
        void PrintBookPublishingHouse();
        void PrintReservationId();
        void PrintHistoryFilters(string Role);
        void PrintUserId();
        void PrintBookQuantity();
        void BookUpdateResult(string resultBookUpdate);
        void PrintFilterModifyBook();
    }
}
