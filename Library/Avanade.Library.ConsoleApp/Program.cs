using Avanade.Library.BusinessLogic;
using System;
using Avanade.Library.DAL;
using System.Xml.Linq;
using Avanade.Library.Entities;

namespace Avanade.Library.ConsoleApp
{
    public static class Program
    {

        static void Main(string[] args)
        {
            IDal xmlDal = new XmlDal();
            IDal dbDal = new DbDal();
            IBusinessLogics controller = new BusinessLogics(xmlDal);
            IViewHandler view = new ViewHandler();

            string username;
            string password;

            string title;
            string authorName;
            string authorSurName;
            string publishingHouse;
            
            int quantity;
            int parsedQuantity;
            
            int BookId;
            int UserId;
            int ReservationId;

            int bookidparse;
            int useridparse;
            int reservationidparse;

            string bookIdBeforeParse;
            string userIdBeforeParse;
            string reservationIdBeforeParse;


            view.GreetingsMessage();

            bool runApp = true;

            view.AskUserName();
            username = Console.ReadLine();
            view.AskPassword();
            password = Console.ReadLine();
            
            IUser user = controller.RequestLogin(username, password);
            string authResult = user.Role;


            while (runApp)
            {

                switch (authResult)
                {

                    case "Admin":

                        view.UserLogin(authResult);
                        view.AdminLogin();
                        view.CloseApplication();

                        switch (Console.ReadLine())
                        {

                            case "1":

                                view.PrintSearchFilters();

                                switch(Console.ReadLine())
                                {
                                    case "0":
                                        title = "";
                                        authorName = "";
                                        authorSurName = "";
                                        publishingHouse = "";
                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.SearchBookAvailables(title, authorName, authorSurName, publishingHouse) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;

                                    case "1":
                                        view.PrintBookTitle();
                                        title = TrimAndLowerConsoleInput();
                                        authorName = "";
                                        authorSurName = "";
                                        publishingHouse = "";
                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.SearchBookAvailables(title, authorName, authorSurName, publishingHouse) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;

                                    case "2":
                                        view.PrintBookAuthorName();
                                        authorName = TrimAndLowerConsoleInput();
                                        view.PrintBookAuthorSurName();
                                        authorSurName = TrimAndLowerConsoleInput();
                                        title = "";
                                        publishingHouse = "";

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.SearchBookAvailables(title, authorName, authorSurName, publishingHouse) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;

                                    case "3":
                                        view.PrintBookPublishingHouse();
                                        publishingHouse = TrimAndLowerConsoleInput();
                                        title = "";
                                        authorName = "";
                                        authorSurName = "";

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.SearchBookAvailables(title, authorName, authorSurName, publishingHouse) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;
                                    default:
                                        Console.WriteLine("Sbagliato tasto! Inserire quelli indicati.");
                                        break;
                                }

                                break;
                                

                            case "2":

                                view.PrintBookId();
                                BookId = int.TryParse(Console.ReadLine(), out bookidparse) ? bookidparse : 0;
                                IReservation reservation = new Reservation();
                                reservation.BookId = bookidparse;
                                reservation.UserId = user.UserId;
                                if (BookId <= 0)
                                {
                                    Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                    break;
                                }
                                Console.Write(controller.RequestReservation(reservation) + "\n");
                                break;

                            case "3":

                                view.PrintReservationId();
                                ReservationId = int.TryParse(Console.ReadLine(), out reservationidparse) ? reservationidparse : 0;

                                if (ReservationId <= 0)
                                {
                                    Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                    break;
                                }
                                Console.Write(controller.RequestCloseReservation(ReservationId) + "\n");
                                break;

                            case "4":

                                view.PrintHistoryFilters(authResult);


                                switch (Console.ReadLine())
                                {
                                    case "0":
                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.RequestReservationHistory(0, 0, 0) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;

                                    case "1":
                                        view.PrintBookId();

                                        bookIdBeforeParse = Console.ReadLine();

                                        if (bookIdBeforeParse == "")
                                        {
                                            bookIdBeforeParse = "0";
                                        }
                                        BookId = int.TryParse(bookIdBeforeParse, out bookidparse) ? bookidparse : -1;

                                        if (BookId <= -1)
                                        {
                                            Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                            break;
                                        }

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.RequestReservationHistory(BookId, 0, 0) + "\n");
                                        Console.WriteLine("\n*************************");

                                        break;

                                    case "2":
                                        view.PrintReservationId();
                                        reservationIdBeforeParse = Console.ReadLine();
                                        if (reservationIdBeforeParse == "")
                                        {
                                            reservationIdBeforeParse = "0";
                                        }
                                        ReservationId = int.TryParse(reservationIdBeforeParse, out reservationidparse) ? reservationidparse : -1;

                                        if (ReservationId <= -1)
                                        {
                                            Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                            break;
                                        }

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.RequestReservationHistory(0, 0, ReservationId) + "\n");
                                        Console.WriteLine("\n*************************");

                                        break;

                                    case "3":
                                        view.PrintUserId();
                                        userIdBeforeParse = Console.ReadLine();
                                        if (userIdBeforeParse == "")
                                        {
                                            userIdBeforeParse = "0";
                                        }
                                        UserId = int.TryParse(userIdBeforeParse, out useridparse) ? useridparse : -1;

                                        if (UserId <= -1)
                                        {
                                            Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero.\n Si prega di ripetere l'operazione." + "\n");
                                            break;
                                        }

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.RequestReservationHistory(0, UserId, 0) + "\n");
                                        Console.WriteLine("\n*************************");

                                        break;

                                    default:
                                        Console.WriteLine("Sbagliato tasto! Inserire quelli indicati.");
                                        break;
                                }

                                break;

                            case "5":

                                view.PrintBookTitle();
                                title = Console.ReadLine();
                                view.PrintBookAuthorName();
                                authorName = Console.ReadLine();
                                view.PrintBookAuthorSurName();
                                authorSurName = Console.ReadLine();
                                view.PrintBookPublishingHouse();
                                publishingHouse = Console.ReadLine();
                                view.PrintBookQuantity();
                                quantity = int.TryParse(Console.ReadLine(), out parsedQuantity) ? parsedQuantity : 0;
                                string resultBook = controller.RequestAddBook(title, authorName, authorSurName, publishingHouse, quantity).ToString();
                                view.AddBookResult(resultBook);
                                break;

                            case "6":
                                string resultBookUpdate;

                                view.PrintBookId();
                                BookId = int.TryParse(Console.ReadLine(), out bookidparse) ? bookidparse : 0;

                                if (BookId <= 0)
                                {
                                    Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                    break;
                                }

                                            view.PrintFilterModifyBook();

                                            view.PrintBookTitle();
                                            title = Console.ReadLine();
                                            view.PrintBookAuthorName();
                                            authorName = Console.ReadLine();
                                            view.PrintBookAuthorSurName();
                                            authorSurName = Console.ReadLine();
                                            view.PrintBookPublishingHouse();
                                            publishingHouse = Console.ReadLine();

                                        if (title.Length != 0 && authorName.Length != 0 && authorSurName.Length != 0 && publishingHouse.Length != 0)
                                        {
                                            resultBookUpdate = controller.RequestUpdateBook(BookId, title, authorName, authorSurName, publishingHouse).ToString();
                                            view.BookUpdateResult(resultBookUpdate);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Attenzione non sono stati inseriti tutti i parametri! Non si può modificare il libro. Riprovare, grazie.");
                                        }

                                break;

                            case "7":

                                view.PrintBookId();
                                BookId = int.TryParse(Console.ReadLine(), out bookidparse) ? bookidparse : 0;

                                if (BookId <= 0)
                                {
                                    Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                    break;
                                }

                                string resultBookDelete = controller.RequestDeleteBook(BookId).ToString();
                                view.BookDeleteResult(resultBookDelete);
                                break;

                            case "x":
                                runApp = false;
                                break;
                            default:
                                Console.WriteLine("Sbagliato tasto! Inserire quelli indicati.");
                                break;
                        }
                        break;

                    case "User":
                        view.UserLogin(authResult);
                        view.CloseApplication();

                        switch (Console.ReadLine())
                        {

                            case "1":

                                view.PrintSearchFilters();

                                switch (Console.ReadLine())
                                {
                                    case "0":
                                        title = "";
                                        authorName = "";
                                        authorSurName = "";
                                        publishingHouse = "";
                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.SearchBookAvailables(title, authorName, authorSurName, publishingHouse) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;

                                    case "1":
                                        view.PrintBookTitle();
                                        title = TrimAndLowerConsoleInput();
                                        authorName = "";
                                        authorSurName = "";
                                        publishingHouse = "";
                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.SearchBookAvailables(title, authorName, authorSurName, publishingHouse) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;
                                    case "2":
                                        view.PrintBookAuthorName();
                                        authorName = TrimAndLowerConsoleInput();
                                        view.PrintBookAuthorSurName();
                                        authorSurName = TrimAndLowerConsoleInput();
                                        title = "";
                                        publishingHouse = "";

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.SearchBookAvailables(title, authorName, authorSurName, publishingHouse) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;
                                    case "3":
                                        view.PrintBookPublishingHouse();
                                        publishingHouse = TrimAndLowerConsoleInput();
                                        title = "";
                                        authorName = "";
                                        authorSurName = "";

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.SearchBookAvailables(title, authorName, authorSurName, publishingHouse) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;
                                    default:
                                        Console.WriteLine("Sbagliato tasto! Inserire quelli indicati.");
                                        break;
                                }

                                break;

                            case "2":

                                view.PrintBookId();
                                BookId = int.TryParse(Console.ReadLine(), out bookidparse) ? bookidparse : 0;
                                IReservation reservation = new Reservation();
                                reservation.BookId = bookidparse;
                                reservation.UserId = user.UserId;
                                if (reservation.BookId <= 0)
                                {
                                    Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                    break;
                                }
                                Console.Write(controller.RequestReservation(reservation) + "\n");
                                break;


                            case "3":

                                view.PrintReservationId();
                                ReservationId = int.TryParse(Console.ReadLine(), out reservationidparse) ? reservationidparse : 0;

                                if (ReservationId <= 0)
                                {
                                    Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                    break;
                                }
                                Console.Write(controller.RequestCloseReservation(ReservationId) + "\n");
                                break;

                            case "4":

                                view.PrintHistoryFilters(authResult);

                                switch (Console.ReadLine())
                                {
                                    case "0":
                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.RequestReservationHistory(0, 0, 0) + "\n");
                                        Console.WriteLine("\n*************************");
                                        break;

                                    case "1":
                                        view.PrintBookId();

                                        bookIdBeforeParse = Console.ReadLine();

                                        if (bookIdBeforeParse == "")
                                        {
                                            bookIdBeforeParse = "0";
                                        }
                                        BookId = int.TryParse(bookIdBeforeParse, out bookidparse) ? bookidparse : -1;

                                        if (BookId <= -1)
                                        {
                                            Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                            break;
                                        }

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.RequestReservationHistory(BookId, 0, 0) + "\n");
                                        Console.WriteLine("\n*************************");

                                        break;

                                    case "2":
                                        view.PrintReservationId();
                                        reservationIdBeforeParse = Console.ReadLine();
                                        if (reservationIdBeforeParse == "")
                                        {
                                            reservationIdBeforeParse = "0";
                                        }
                                        ReservationId = int.TryParse(reservationIdBeforeParse, out reservationidparse) ? reservationidparse : -1;

                                        if (ReservationId <= -1)
                                        {
                                            Console.WriteLine("Attenzione il codice inserito non è valido!  \n Dev'essere un numero intero maggiore di zero. \n Si prega di ripetere l'operazione." + "\n");
                                            break;
                                        }

                                        Console.WriteLine("*************************\n");
                                        Console.Write(controller.RequestReservationHistory(0, 0, ReservationId) + "\n");
                                        Console.WriteLine("\n*************************");

                                        break;

                                    default:
                                        Console.WriteLine("Sbagliato tasto! Inserire quelli indicati.");
                                        break;
                                }
                                
                                break;
                            case "x":
                                runApp = false;
                                break;
                            default:
                                Console.WriteLine("Sbagliato tasto! Inserire quelli indicati.");
                                break;
                        }
                        break;

                    case "Eccezione":
                        
                        Console.WriteLine("Non è stato possibile proseguire nella richiesta. La risorsa non è stata trovata. \n");
                        Console.WriteLine("Si prega di riprovare più tardi, premere qualsiasi tasto per uscire.");
                        Console.ReadLine();
                        runApp = false;
                        break;

                    default:

                        Console.WriteLine("Attenzione! Le credenziali sono errate! Scegliere se uscire dall'app o riprovare:");
                        Console.WriteLine("\t premere [q] e invio per Uscire");
                        Console.WriteLine("\t premere qualsiasi tasto tranne [q] e invio per Riprovare");

                        string input = TrimAndLowerConsoleInput();
                        if (input == "q")
                            runApp = false;
                        else
                        {
                            view.AskUserName();
                            username = Console.ReadLine();
                            view.AskPassword();
                            password = Console.ReadLine();
                            authResult = controller.RequestLogin(username, password).ToString();
                        }
                        break;

                }

            }

        }

        static string TrimAndLowerConsoleInput()
        {
            return Console.ReadLine().ToLower().Trim();
        }
    }

}

