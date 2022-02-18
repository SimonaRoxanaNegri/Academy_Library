using System;

namespace Avanade.Library.ConsoleApp
{
    public class ViewHandler : IViewHandler
    {
        
        public void GreetingsMessage()
        {
            Console.WriteLine("Benvenuti nella Biblioteca!\r");
            Console.WriteLine("------------------------\n");
        }

        public void AskUserName()
        {
            Console.WriteLine("Inserire le credenziali per fare il login:");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Digitare l'username e premere invio");
        }

        public void AskPassword()
        {
            Console.WriteLine("Digitare la password e premere invio:");
        }


        public void UserLogin(string Role)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Digitare il tasto [1] se si vuole cercare un libro");
            Console.WriteLine("Digitare il tasto [2] se si vuole prendere in prestito un libro");
            Console.WriteLine("Digitare il tasto [3] se si vuole restituire un libro");
            Console.WriteLine("Digitare il tasto [4] se si vuole vedere la lista delle prenotazioni effettuate" + (Role == "Admin" ? " da un utente" : " finora"));
        }

        public void AdminLogin()
        {
            Console.WriteLine("Digitare il tasto [5] se si vuole inserire un libro");
            Console.WriteLine("Digitare il tasto [6] se si vuole modificare un libro");
            Console.WriteLine("Digitare il tasto [7] se si vuole cancellare un libro");
        }
        public void CloseApplication()
        {
            Console.WriteLine("Digitare il tasto [x] se si vuole uscire dall'applicazione");
        }

        public void PrintBookTitle()
        {
            Console.WriteLine("Inserire il titolo");
        }
        public void PrintBookAuthorName()
        {
            Console.WriteLine("Inserire il nome dell'autore");
        }

        public void PrintSearchFilters()
        {
            Console.WriteLine("Si può ricercare per 'titolo', 'autore' o 'casa editrice'.\n");

            Console.WriteLine("Digitare il tasto [0] per vedere tutti i libri senza filtro.");
            Console.WriteLine("Digitare il tasto [1] per il filtro 'titolo'");
            Console.WriteLine("Digitare il tasto [2] per il filtro 'autore'");
            Console.WriteLine("Digitare il tasto [3] per il filtro 'casa editrice'");
        }

        public void PrintHistoryFilters(string Role)
        {
            Console.WriteLine("Si può ricercare per 'codice libro', 'codice prenotazione',\n"
                + (Role == "Admin" ? "'codice utente' o senza specificare un filtro, premendo '0'." : "o senza specificare un filtro, premendo '0'.") + "\n");
            
            Console.WriteLine("Digitare il tasto [0] per vedere tutti i libri senza filtro.");
            Console.WriteLine("Digitare il tasto [1] per il filtro 'codice libro'");
            Console.WriteLine("Digitare il tasto [2] per il filtro 'codice prenotazione'");
            Console.WriteLine((Role == "Admin" ? "Digitare il tasto [3] per il filtro 'codice utente'" : ""));
        }

        public void PrintBookAuthorSurName()
        {
            Console.WriteLine("Inserire il cognome dell'autore");
        }
        public void PrintBookPublishingHouse()
        {
            Console.WriteLine("Inserire la casa editrice");
        }
        public void PrintBookQuantity()
        {
            Console.WriteLine("Inserire la quantità");
        }

        public void AddBookResult(string Result)
        {
            Console.WriteLine(Result);

        }

        public void BookUpdateResult(string Result)
        {
            Console.WriteLine(Result);
        }

        public void PrintBookId()
        {
            Console.WriteLine("Inserire il codice del libro");
        }

        public void PrintUserId()
        {
            Console.WriteLine("Inserire il codice utente");
        }

        public void PrintReservationId()
        {
            Console.WriteLine("Inserire il codice della prenotazione");
        }

        public void BookDeleteResult(string resultBookDelete)
        {
            Console.WriteLine(resultBookDelete);
        }

        public void PrintFilterModifyBook()
        {
            Console.WriteLine("Si può modificare per 'titolo', 'nome autore', 'cognome autore', e 'casa editrice'.\n");
        }
    }
}
