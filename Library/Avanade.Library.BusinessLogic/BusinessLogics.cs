using Avanade.Library.DAL;
using Avanade.Library.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Avanade.Library.BusinessLogic
{
    public class BusinessLogics : IBusinessLogics
    {
        readonly private DateTime StartDate; 
        private IDal dal;

        public BusinessLogics(IDal dal)
        {
            this.dal = dal;
            this.StartDate = DateTime.Now;
        }

        public IUser RequestLogin(string Username, string Password)
        {
            IUser user;

                user = dal.GetUser(Username, Password);


            return user;
        }

        public List<IBooksAvailables> SearchBookAvailables(string title, string authorName, string authorSurName, string publishingHouse)
        {
            List<IBooksAvailables> listBookAvailables;

            listBookAvailables = dal.GetBooksAvailables(title, authorName, authorSurName, publishingHouse);
            
            if (listBookAvailables.Count > 0)
            {
                return listBookAvailables;
            }
            else
            {
                return new List<IBooksAvailables>();
            }

        }

        public List<IReservationsHistory> RequestReservationHistory(int BookId, int UserId, int ReservationId)
        {
            
            List<IReservationsHistory> reservations;

            reservations = dal.GetHistoryReservations(BookId, UserId, ReservationId);

            if (reservations.Count > 0)
            {
                return reservations;
            }
            else
            {
                return new List<IReservationsHistory>();
            }

        }

        public IResponse<IBook> RequestAddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity)
        {
            IBook book;
            int newQuantity;
            int bookId;
            bool replyAddBook;
            IResponse<IBook> message = new Response<IBook>();
            if (Title.Length == 0 || AuthorName.Length == 0 || AuthorSurName.Length == 0 || PublishingHouse.Length == 0 || Quantity == 0)
            {
                message.ResponseMessage = "Attenzione non sono stati inseriti tutti i parametri! Non si può aggiungere il libro. Riprovare, grazie.";
                return message;
            }

            try
            {
                bookId = dal.GetBookId(Title, AuthorName, AuthorSurName, PublishingHouse);
                book = dal.GetBookById(bookId);

                if (bookId > 0)
                {
                    
                    newQuantity = dal.UpdateQuantityOfBook(bookId, Quantity + book.Quantity);
                    message.ResponseMessage = $"Il libro {Title} è già presente. E' stata aggiornata la quantità da: {book.Quantity} a: {newQuantity}.";
                    book.Quantity = newQuantity;
                    message.entity = book;
                }
                else
                {
                    replyAddBook = dal.AddBook(Title, AuthorName, AuthorSurName, PublishingHouse, Quantity);
                    message.entity = book;
                    message.ResponseMessage = $"Il libro {Title} è stato aggiunto correttamente!";
                }
                return message;
            }

            catch (NullReferenceException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (DirectoryNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (FileNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }

        }

        public IResponse<IBook> RequestUpdateBook(int BookId, string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {
            int duplicateBookId;

            IBook bookUpdated;
            IResponse<IBook> message = new Response<IBook>();
            try
            {
                bookUpdated = dal.GetBookById(BookId);
                duplicateBookId = dal.GetDuplicateBook(Title, AuthorName, AuthorSurName, PublishingHouse);

                if (bookUpdated.Quantity >= 1 && duplicateBookId == 0)
                {
                    bookUpdated = dal.UpdateBook(BookId, Title, AuthorName, AuthorSurName, PublishingHouse);
                    message.entity = bookUpdated;
                    message.ResponseMessage = $"\nIl libro {Title} è stato modificato correttamente! \n" +
                        $"Di seguito i dati aggiornati che hai inserito: \n\n {bookUpdated.Title}, \n {bookUpdated.AuthorName}, \n {bookUpdated.AuthorSurname}, \n {bookUpdated.PublishingHouse}";
                    
                }
                else if (bookUpdated.Quantity < 1)
                {
                    message.entity = bookUpdated;
                    message.ResponseMessage = $"Il libro con codice: {BookId} non si può modificare perché non esiste.";
                }
                else
                {
                    message.entity = bookUpdated;
                    message.ResponseMessage = "Stai inserendo gli stessi dati creando così un duplicato! Riprovare l'operazione.";
                }
                return message;

            }
            catch (NullReferenceException ex )
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (DirectoryNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (FileNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }

        }

        public ReservationsHistory GetReservationsInProgress(List<IReservationsHistory> reservationList)
        {
            ReservationsHistory reservationsInProgress = (ReservationsHistory)
                 reservationList.Where(x => x.EndDate > DateTime.Now).OrderBy(h => h.EndDate).LastOrDefault();
            return reservationsInProgress;
        }

    /*    public ReservationsHistory GetReservationsAvailable(List<IReservationsHistory> reservationList)
        {
            ReservationsHistory reservationsAvailable = (ReservationsHistory)reservationList.FirstOrDefault(x =>
            (x.Quantity == reservationList.Count(book => book.EndDate > DateTime.Now);
            return reservationsAvailable;
        }
    */
        public IResponse<IBook> RequestDeleteBook(int BookId)
        {
            IReservationsHistory reservationStatus;
            IResponse<IBook> message = new Response<IBook>();
            bool replyDeletebook;
            bool replyDeleteReservation;

            try
            {
                IBook book = dal.GetBookById(BookId);
                reservationStatus = GetReservationsInProgress(dal.GetReservationByBookId(BookId));

                
                if (book.Quantity < 1)
                {
                    message.ResponseMessage = $"Il libro con codice: {BookId} non si può cancellare perché non esiste. Si prega di ripetere l'operazione. \n";
                    message.entity = new Book();
                    return message;
                }

                if (reservationStatus == null)
                {
                    replyDeletebook = dal.DeleteBook(BookId);
                    replyDeleteReservation = dal.DeleteReservation(BookId);
                    message.ResponseMessage = $"Il libro con codice: {BookId} è stato cancellato con successo!";
                    return message;

                }
                else
                {
                     message.ResponseMessage = $"La cancellazione non è stata effettuata in quanto il libro {reservationStatus.Title} risulta essere ancora prenotato " +
                           $"dall’utente {reservationStatus.Username} a partire dal {reservationStatus.StartDate.ToString("dd/MM/yyyy")} sino al {reservationStatus.EndDate.ToString("dd/MM/yyyy")} ";
                    return message;
                }
            }
            catch (NullReferenceException ex )
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (DirectoryNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (FileNotFoundException ex )
            {
                message.ResponseMessage = ex.Message;
                return message;
            }

        }

        public IResponse<IReservationsHistory> RequestReservation(IReservation Reservation)
        {

            IBook book;
            List<IReservationsHistory> listOfReservation;
            IReservationsHistory reservationStatus;
            IResponse<IReservationsHistory> message = new Response<IReservationsHistory>();
            int reservationId;

            try
            {
                book = dal.GetBookById(Reservation.BookId);
                listOfReservation = dal.GetReservationByBookId(Reservation.BookId);
                reservationStatus = listOfReservation.FirstOrDefault(reservation => reservation.Reserved && reservation.UserId == Reservation.UserId);

                if (book.Quantity < 1)
                {
                    message.ResponseMessage = $"Il libro con id: {Reservation.BookId} non si può prenotare perché non esiste. Si prega di ripetere l'operazione.";
                    message.entity = reservationStatus;
                    return message;
                }

                else if (reservationStatus == null)
                {
                    reservationId = dal.AddReservation(Reservation.UserId, Reservation.BookId, Reservation.StartDate, Reservation.StartDate);
                    message.ResponseMessage = $"Prenotato il libro con successo, con il codice prenotazione seguente: {reservationId}";
                    message.entity = reservationStatus;
                    return message;
                }

                else if (reservationStatus.Reserved)
                {
                    message.ResponseMessage = $"La prenotazione non è andata a buon fine, perché non è possibile prenotare un libro di cui hai " +
                        $"già una prenotazione attiva.";
                    message.entity = reservationStatus;
                    return message;
                }
                else if (reservationStatus.Quantity == listOfReservation.Count(reservation => reservation.EndDate > DateTime.Now))
                {
                    message.ResponseMessage = "Non è possibile prenotare il libro perché tutte le copie sono prenotate, riprovare quando una prenotazione si libererà.";
                    message.entity = reservationStatus;
                    return message;
                }

                else if (reservationStatus.EndDate > DateTime.Now)
                {
                    message.ResponseMessage = $"La prenotazione non è andata a buon fine in quanto il libro {reservationStatus.Title} " +
                           $"risulta essere ancora prenotato sino al {reservationStatus.EndDate.ToString("dd/MM/yyyy")}";
                   message.entity = reservationStatus;
                    return message;
                }
                else
                {
                    message.ResponseMessage = "Non è stato possibile prenotare il libro, perché non è stato trovato.";
                    message.entity = reservationStatus;
                    return message;
                }

            }
            catch (NullReferenceException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            } 
            catch (DirectoryNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (FileNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }

        }

        public IResponse<IReservationsHistory> RequestCloseReservation(int ReservationId)
        {
            int quantityOfDaysReservation;
            IReservationsHistory reservationStatus;
            IResponse<IReservationsHistory> message = new Response<IReservationsHistory>();
            try
            {
                
                reservationStatus = GetReservationsInProgress(dal.GetReservationByReservationId(ReservationId));
                quantityOfDaysReservation = dal.GetReservationDays(ReservationId);

                if (reservationStatus != null)
                {
                    dal.UpdateEndDateReservation(ReservationId);
                    message.ResponseMessage = $"Il libro è stato restituito con successo!";
                    message.entity = reservationStatus;
                    return message;
                }
                else if (quantityOfDaysReservation > 30)
                {
                    dal.UpdateEndDateReservation(ReservationId);
                    message.ResponseMessage = $"Il codice prenotazione {ReservationId} da lei inoltrato NON corrisponde a un prestito attualmente in corso!";
                    message.entity = reservationStatus;
                    return message;
                }
                else
                {
                    message.ResponseMessage = $"Il libro con codice prenotazione: {ReservationId} non risulta essere attualmente in prestito.";
                    message.entity = reservationStatus;
                    return message ;
                }
            }
            catch (NullReferenceException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (DirectoryNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }
            catch (FileNotFoundException ex)
            {
                message.ResponseMessage = ex.Message;
                return message;
            }

        }

    }
}
