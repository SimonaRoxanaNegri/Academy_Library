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
        readonly static private string errorMessage = "Non è stato possibile proseguire nella richiesta. Si prega di riprovare. \n " +
            "Se il problema persiste, contattare la nostra assistenza al numero verde : 800 146521.";

        private int Id;
        private string Role;
        private IDal dal;

        public BusinessLogics(IDal dal)
        {
            this.dal = dal;
            this.StartDate = DateTime.Now;
        }

        public string RequestLogin(string Username, string Password)
        {
            IUser user;

            try
            {
                user = dal.GetUser(Username, Password);
                this.Id = user.UserId;
                this.Role = user.Role;
            }
            catch (NullReferenceException)
            {
                return errorMessage;
            }
            catch (DirectoryNotFoundException)
            {
                return errorMessage;
            }
            catch (FileNotFoundException)
            {
                return errorMessage;
            }

            return user.Role;
        }

        public string SearchBookAvailables(string title, string authorName, string authorSurName, string publishingHouse)
        {
            List<IBooksAvailables> listBookAvailables;

            try
            {
                listBookAvailables = dal.GetBooksAvailables(title, authorName, authorSurName, publishingHouse);
            }
            catch (NullReferenceException)
            {
                return errorMessage;
            }
            catch (DirectoryNotFoundException)
            {
                return errorMessage;
            }
            catch (FileNotFoundException)
            {
                return errorMessage;
            }

            if (listBookAvailables.Count > 0)
            {
                string bookAvailables = String.Join(",\n", listBookAvailables.Select(x => x.FormatterBookAvailableString()));
                return bookAvailables;
            }
            else
            {
                return "Non sono stati trovati libri disponibili, con i criteri inseriti.";
            }

        }

        public string RequestReservationHistory(int BookId, int UserId, int ReservationId)
        {
            string message;
            List<IReservationsHistory> reservations;

            try
            {
                reservations = dal.GetHistoryReservations(BookId, (this.Role == "Admin" ? UserId : this.Id), ReservationId);
            }
            catch (NullReferenceException)
            {
                return errorMessage;
            }
            catch (DirectoryNotFoundException)
            {
                return errorMessage;
            }
            catch (FileNotFoundException)
            {
                return errorMessage;
            }

            if (reservations.Count > 0)
            {
                message = String.Join(",\n", reservations.Select(x => x.FormatterReservationHistoryString()));
            }
            else
            {
                message = "Non ci sono libri prenotati.";
            }
            return message;
        }

        public string RequestAddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity)
        {
            int oldQuantity;
            int newQuantity;
            int bookId;
            

            string response;
            if (Title.Length == 0 || AuthorName.Length == 0 || AuthorSurName.Length == 0 || PublishingHouse.Length == 0 || Quantity == 0)
            {
                return "Attenzione non sono stati inseriti tutti i parametri! Non si può aggiungere il libro. Riprovare, grazie.";
            }

            try
            {
                bookId = dal.GetBookId(Title, AuthorName, AuthorSurName, PublishingHouse);
                oldQuantity = dal.GetQuantityBooksById(bookId);
                
            if (bookId > 0)
            {
                newQuantity = dal.UpdateQuantityOfBook(bookId, Quantity + oldQuantity);
                response = $"Il libro {Title} è già presente. E' stata aggiornata la quantità da: {oldQuantity} a: {newQuantity}.";
            }
            else
            {
                dal.AddBook(Title, AuthorName, AuthorSurName, PublishingHouse, Quantity);
                response = $"Il libro {Title} è stato aggiunto correttamente!";
            }
            return response;
            }

            catch (NullReferenceException)
            {
                return errorMessage;
            }
            catch (DirectoryNotFoundException)
            {
                return errorMessage;
            }
            catch (FileNotFoundException)
            {
                return errorMessage;
            }

        }

        public string RequestUpdateBook(int BookId, string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {
            int quantityOfBook;
            int duplicateBookId;
            string response;
            IBook bookUpdated;

            try
            {
                quantityOfBook = dal.GetQuantityBooksById(BookId);
                duplicateBookId = dal.GetDuplicateBook(Title, AuthorName, AuthorSurName, PublishingHouse);

                if (quantityOfBook >= 1 && duplicateBookId == 0)
                {
                    bookUpdated = dal.UpdateBook(BookId, Title, AuthorName, AuthorSurName, PublishingHouse);
                    response = $"\nIl libro {Title} è stato modificato correttamente! \n" +
                        $"Di seguito i dati aggiornati che hai inserito: \n\n {bookUpdated.Title}, \n {bookUpdated.AuthorName}, \n {bookUpdated.AuthorSurname}, \n {bookUpdated.PublishingHouse}";
                }
                else if (quantityOfBook < 1)
                {
                    response = $"Il libro con codice: {BookId} non si può modificare perché non esiste.";
                }
                else
                {
                    response = "Stai inserendo gli stessi dati creando così un duplicato! Riprovare l'operazione.";
                }
                return response;

            }
            catch (NullReferenceException)
            {
                return errorMessage;
            }
            catch (DirectoryNotFoundException)
            {
                return errorMessage;
            }
            catch (FileNotFoundException)
            {
                return errorMessage;
            }

        }

        public ReservationsHistory GetReservationsInProgress(List<IReservationsHistory> reservationList)
        {
            ReservationsHistory reservationsInProgress = (ReservationsHistory)reservationList.Where(x => x.EndDate > DateTime.Now && x.UserId == this.Id).OrderBy(h => h.EndDate).LastOrDefault();
            return reservationsInProgress;
        }

        public ReservationsHistory GetReservationsAvailable(List<IReservationsHistory> reservationList)
        {
            ReservationsHistory reservationsAvailable = (ReservationsHistory)reservationList.FirstOrDefault(x =>
            (x.Quantity == reservationList.Count(book => book.EndDate > DateTime.Now)) || (x.UserId == this.Id && x.EndDate > DateTime.Now));
            return reservationsAvailable;
        }

        public string RequestDeleteBook(int BookId)
        {

            int quantityOfBook;
            IReservationsHistory reservationStatus;

            try
            {
                quantityOfBook = dal.GetQuantityBooksById(BookId);
                reservationStatus = GetReservationsInProgress(dal.GetReservationByBookId(BookId));

                if (quantityOfBook < 1)
                {
                    return $"Il libro con codice: {BookId} non si può cancellare perché non esiste. Si prega di ripetere l'operazione. \n";
                }

                if (reservationStatus == null)
                {
                    dal.DeleteBook(BookId);
                    dal.DeleteReservation(BookId);
                    return $"Il libro con codice: {BookId} è stato cancellato con successo!";
                }
                else
                {
                    return $"La cancellazione non è stata effettuata in quanto il libro {reservationStatus.Title} risulta essere ancora prenotato " +
                           $"dall’utente {reservationStatus.Username} a partire dal {reservationStatus.StartDate.ToString("dd/MM/yyyy")} sino al {reservationStatus.EndDate.ToString("dd/MM/yyyy")} ";
                }
            }
            catch (NullReferenceException)
            {
                return errorMessage;
            }
            catch (DirectoryNotFoundException)
            {
                return errorMessage;
            }
            catch (FileNotFoundException)
            {
                return errorMessage;
            }

        }

        public string RequestReservation(int bookId)
        {

            int quantityOfBook;
            List<IReservationsHistory> listOfReservation;
            IReservationsHistory reservationStatus;
            int reservationId;

            try
            {
                quantityOfBook = dal.GetQuantityBooksById(bookId);
                listOfReservation = dal.GetReservationByBookId(bookId);
                reservationStatus = GetReservationsAvailable(dal.GetReservationByBookId(bookId));

                if (quantityOfBook < 1)
                {
                    return $"Il libro con id: {bookId} non si può prenotare perché non esiste. Si prega di ripetere l'operazione.";
                }

                else if (reservationStatus == null)
                {
                    reservationId = dal.AddReservation(this.Id, bookId, this.StartDate, this.StartDate);
                    return $"Prenotato il libro con successo, con il codice prenotazione seguente: {reservationId}";
                }

                else if (reservationStatus.UserId == this.Id && reservationStatus.EndDate > DateTime.Now)
                {
                    return $"La prenotazione non è andata a buon fine, perché non è possibile prenotare un libro di cui hai " +
                        $"già una prenotazione attiva.";
                }

                else if (reservationStatus.Quantity == listOfReservation.Where(book => book.EndDate > DateTime.Now).Count())
                {
                    return "Non è possibile prenotare il libro perché tutte le copie sono prenotate, riprovare quando una prenotazione si libererà.";
                }

                else if (reservationStatus.EndDate > DateTime.Now)
                {
                    return $"La prenotazione non è andata a buon fine in quanto il libro {reservationStatus.Title} " +
                           $"risulta essere ancora prenotato sino al {reservationStatus.EndDate.ToString("dd/MM/yyyy")}";
                }
                else
                {
                    return "Non è stato possibile prenotare il libro, perché non è stato trovato.";
                }

            }
            catch (NullReferenceException)
            {
                return errorMessage;
            }
            catch (DirectoryNotFoundException)
            {
                return errorMessage;
            }
            catch (FileNotFoundException)
            {
                return errorMessage;
            }

        }

        public string RequestCloseReservation(int ReservationId)
        {
            int quantityOfDaysReservation;
            IReservationsHistory reservationStatus;

            try
            {
                reservationStatus = GetReservationsInProgress(dal.GetReservationByReservationId(ReservationId));
                quantityOfDaysReservation = dal.GetReservationDays(ReservationId);

                if (reservationStatus != null)
                {
                    dal.UpdateEndDateReservation(ReservationId);
                    return $"Il libro è stato restituito con successo!";
                }
                else if (quantityOfDaysReservation > 30)
                {
                    dal.UpdateEndDateReservation(ReservationId);
                    return $"Il codice prenotazione {ReservationId} da lei inoltrato NON corrisponde a un prestito attualmente in corso!";
                }
                else
                {
                    return $"Il libro con codice prenotazione: {ReservationId} non risulta essere attualmente in prestito.";
                }
            }
            catch (NullReferenceException)
            {
                return errorMessage;
            }
            catch (DirectoryNotFoundException)
            {
                return errorMessage;
            }
            catch (FileNotFoundException)
            {
                return errorMessage;
            }

        }

    }
}
