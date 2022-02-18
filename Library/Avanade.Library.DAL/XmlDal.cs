using Avanade.Library.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Avanade.Library.DAL
{
    public class XmlDal : IDal
    {

        public IUser GetUser(string Username, string Password)
        {
            IUser user = GetUsersFromDataSource().listOf.FirstOrDefault(u => u.Username == Username && u.Password == Password);
            return user ?? new User();
        }

        public List<IBooksAvailables> GetBooksAvailables(string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {

            IBooks books = GetBooksFromDataSource();
            IReservations reservations = GetReservationsFromDataSource();

            List<IBooksAvailables> bookStatus = (from book in books.listOf.Where(book =>
                                                                 (TrimAndLowerString(book.Title) == Title || Title == "")
                                                                 && (TrimAndLowerString(book.AuthorName) == AuthorName || AuthorName == "")
                                                                 && (TrimAndLowerString(book.AuthorSurname) == AuthorSurName || AuthorSurName == "")
                                                                 && (TrimAndLowerString(book.PublishingHouse) == PublishingHouse || PublishingHouse == ""))
                                               join reservation in reservations.listOf on book.BookId equals reservation.BookId into leftjoin
                                               from newbook in leftjoin.DefaultIfEmpty()
                                               select (IBooksAvailables)new BooksAvailables(
                                               book.BookId,
                                               book.Title,
                                               book.AuthorName,
                                               book.AuthorSurname,
                                               book.PublishingHouse,
                                               (newbook != null) ? newbook.EndDate : DateTime.Now)).ToList();

            List<IBooksAvailables> firstAvailableBooks = bookStatus
                                               .GroupBy(h => h.BookId)
                                               .Select(group => group.OrderBy(book =>book.DateAvailable))
                                               .Select(f => f.FirstOrDefault())
                                               .ToList();
            return firstAvailableBooks;
        }


        public List<IReservationsHistory> GetHistoryReservations(int BookId, int UserId, int ReservationId)
        {
            IUsers users = GetUsersFromDataSource();
            IBooks books = GetBooksFromDataSource();
            IReservations reservations = GetReservationsFromDataSource();

            List<IReservationsHistory> historyReservationsStatus = (from user in users.listOf.Where(user => (user.UserId == UserId || UserId == 0))
                                                                  join reservation in reservations.listOf.Where(reservation => (reservation.ReservationId == ReservationId || ReservationId == 0))
                                                                  on user.UserId equals reservation.UserId
                                                                  join book in books.listOf.Where(book => (book.BookId == BookId || BookId == 0))
                                                                  on reservation.BookId equals book.BookId
                                                                  select (IReservationsHistory)new ReservationsHistory(
                                                                  book.BookId,
                                                                  book.Title,
                                                                  user.Username,
                                                                  reservation.StartDate,
                                                                  reservation.EndDate,
                                                                  reservation.ReservationId,
                                                                  ((DateTime.Now - reservation.StartDate).Days < 30) && (reservation.EndDate > DateTime.Now))).ToList();
            return historyReservationsStatus;
        }

        public bool AddBook(string Title, string AuthorName, string AuthorSurName, string PublishingHouse, int Quantity)
        {
            try
            {
            IBooks books = GetBooksFromDataSource();
            XElement DocXml = GetDataSource();
            XElement bookToAdd =
                new XElement("Book",
                new XAttribute("BookId", books.listOf.Count + 1),
                new XAttribute("Title", Title),
                new XAttribute("AuthorName", AuthorName),
                new XAttribute("AuthorSurname", AuthorSurName),
                new XAttribute("Publisher", PublishingHouse),
                new XAttribute("Quantity", Quantity)
                );
            DocXml.Element("Books").Add(bookToAdd);
            SaveDataSource(DocXml);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public int GetBookId(string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {
            IBooks books = GetBooksFromDataSource();
            int bookId = (from book in books.listOf.Where(book => (TrimAndLowerString(book.Title) == TrimAndLowerString(Title))
                                                    && (TrimAndLowerString(book.AuthorName) == TrimAndLowerString(AuthorName))
                                                    && (TrimAndLowerString(book.AuthorSurname) == TrimAndLowerString(AuthorSurName))
                                                    && (TrimAndLowerString(book.PublishingHouse) == TrimAndLowerString(PublishingHouse)))
                          select book.BookId).FirstOrDefault();
            return bookId;
        }

        public int UpdateQuantityOfBook(int BookId, int Quantity)
        {
            XElement DocXml = GetDataSource();
            DocXml.Descendants("Book")
                .FirstOrDefault(book => book.Attribute("BookId").Value == BookId.ToString())
                .Attribute("Quantity").Value = Quantity.ToString();
            SaveDataSource(DocXml);
            return Quantity;
        }

        public IBook UpdateBook(int BookId, string Title, string AuthorName, string AuthorSurName, string PublishingHouse)
        {
            XElement DocXml = GetDataSource();
            XElement bookToUpdate = DocXml.Descendants("Book")
                .FirstOrDefault(book => book.Attribute("BookId").Value == BookId.ToString());

            string titleUpdated = bookToUpdate.Attribute("Title").Value = Title == "" ? bookToUpdate.Attribute("Title").Value : Title;
            string AuthorNameUpdated = bookToUpdate.Attribute("AuthorName").Value = AuthorName == "" ? bookToUpdate.Attribute("AuthorName").Value : AuthorName;
            string AuthorSurNameUpdated = bookToUpdate.Attribute("AuthorSurname").Value = AuthorSurName == "" ? bookToUpdate.Attribute("AuthorSurname").Value : AuthorSurName;
            string PublishingHousetitleUpdated = bookToUpdate.Attribute("Publisher").Value = PublishingHouse == "" ? bookToUpdate.Attribute("Publisher").Value : PublishingHouse;

            IBook bookUpdated = new Book(titleUpdated, AuthorNameUpdated, AuthorSurNameUpdated, PublishingHousetitleUpdated);
            SaveDataSource(DocXml);
            return bookUpdated;
        }

        public IBook GetBookById(int bookId)
        {
            IBooks books = GetBooksFromDataSource();
            IEnumerable<IBook> numberOfQuantity = from book in books.listOf.Where(book => book.BookId == bookId)
                                                select book;
            return numberOfQuantity.FirstOrDefault();
        }

        public int GetDuplicateBook(string title, string authorName, string authorSurName, string publishingHouse)
        {
            IBooks book = GetBooksFromDataSource();
            IEnumerable<int> duplicate = from b in book.listOf.Where(z => TrimAndLowerString(z.Title) == TrimAndLowerString(title) &&
                                            TrimAndLowerString(z.AuthorName) == TrimAndLowerString(authorName) &&
                                            TrimAndLowerString(z.AuthorSurname) == TrimAndLowerString(authorSurName) &&
                                            TrimAndLowerString(z.PublishingHouse) == TrimAndLowerString(publishingHouse))
                                         select b.BookId;
            return duplicate.FirstOrDefault();
        }

        public bool DeleteBook(int BookId)
        {
            try
            {
                XElement DocXml = GetDataSource();
                DocXml.Descendants("Book").Where(o => o.Attribute("BookId").Value == BookId.ToString()).Remove();
                SaveDataSource(DocXml);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public int AddReservation(int userId, int bookId, DateTime startDate, DateTime endDate)
        {
            int reservationIdIncrement = GetReservationsFromDataSource().listOf.Count + 1;
            XElement DocXml = GetDataSource();
            XElement reservationToAdd = new XElement("Reservation",
                new XAttribute("ReservationId", reservationIdIncrement),
                new XAttribute("BookId", bookId),
                new XAttribute("UserId", userId),
                new XAttribute("StartDate", startDate.ToString("dd/MM/yyyy")),
                new XAttribute("EndDate", endDate.AddDays(30).ToString("dd/MM/yyyy"))
                );
            DocXml.Element("Reservations").Add(reservationToAdd);
            SaveDataSource(DocXml);
            return reservationIdIncrement;
        }

        public bool DeleteReservation(int BookId)
        {
            try
            {
                XElement DocXml = GetDataSource();
                DocXml.Descendants("Reservation").Where(reservation => reservation.Attribute("BookId").Value == BookId.ToString()).Remove();
                SaveDataSource(DocXml);
            }
            catch (Exception)
            {
                return false;
            }

            return true;

            
        }

        public string UpdateEndDateReservation(int ReservationId)
        {
            XElement DocXml = GetDataSource();
            XElement updateEndDate = DocXml.Descendants("Reservation")
                .FirstOrDefault(reservation => (reservation.Attribute("ReservationId").Value == ReservationId.ToString()));
            updateEndDate.Attribute("EndDate").Value = DateTime.Now.ToString("dd/MM/yyyy");
            SaveDataSource(DocXml);
            return DateTime.Now.ToString("dd/MM/yyyy");
        }

        public int GetReservationDays(int reservationId)
        {
            IReservations reservations = GetReservationsFromDataSource();
            int quantityOfDays = (from reservation in reservations.listOf.Where(reservation => (reservation.ReservationId == reservationId))
                                  where (reservation.EndDate > DateTime.Now)
                                  select (reservation.EndDate - reservation.StartDate).Days).FirstOrDefault();
            return quantityOfDays;
        }

        public List<IReservationsHistory> GetReservationByBookId(int bookId)
        {
            IUsers users = GetUsersFromDataSource();
            IBooks books = GetBooksFromDataSource();
            IReservations reservations = GetReservationsFromDataSource();
            List<IReservationsHistory> reservationStatus = (from reservation in reservations.listOf.Where(reservation => reservation.BookId == bookId)
                                                          join book in books.listOf
                                                          on reservation.BookId equals book.BookId
                                                          join user in users.listOf
                                                          on reservation.UserId equals user.UserId
                                                          select (IReservationsHistory)new ReservationsHistory(
                                                              user.UserId,
                                                              book.BookId,
                                                              book.Title,
                                                              user.Username,
                                                              reservation.StartDate,
                                                              reservation.EndDate,
                                                              reservation.ReservationId,
                                                              book.Quantity,
                                                              ((DateTime.Now - reservation.StartDate).Days < 30) && (reservation.EndDate > DateTime.Now))).ToList();
            return reservationStatus;
        }

        public List<IReservationsHistory> GetReservationByReservationId(int ReservationId)
        {
            IUsers users = GetUsersFromDataSource();
            IBooks books = GetBooksFromDataSource();
            IReservations reservations = GetReservationsFromDataSource();
            List<IReservationsHistory> reservationStatus = (from reservation in reservations.listOf.Where(reservation => reservation.ReservationId == ReservationId)
                                                          join book in books.listOf
                                                          on reservation.BookId equals book.BookId
                                                          join user in users.listOf
                                                          on reservation.UserId equals user.UserId
                                                          select (IReservationsHistory)new ReservationsHistory(
                                                              user.UserId,
                                                              book.BookId,
                                                              book.Title,
                                                              user.Username,
                                                              reservation.StartDate,
                                                              reservation.EndDate,
                                                              reservation.ReservationId,
                                                              book.Quantity,
                                                              ((DateTime.Now - reservation.StartDate).Days < 30) && (reservation.EndDate > DateTime.Now))).ToList();
            return reservationStatus;
        }

        private string TrimAndLowerString(string param)
        {
            return param.Trim().ToLower();
        }

        private XElement GetDataSource()
        {
            XElement fileXml;
            fileXml = XElement.Load(ConfigurationManager.ConnectionStrings["DirectoryPath"].ConnectionString);
            return fileXml;
        }
        private void SaveDataSource(XElement DocXml)
        {
            DocXml.Save(ConfigurationManager.ConnectionStrings["DirectoryPath"].ConnectionString);
        }

        private IUsers GetUsersFromDataSource()
        {
            DataMapper converterXML = new DataMapper();
            IUsers users;
            users = (IUsers)converterXML.DeserializeObject(GetDataSource().Element("Users").ToString(), typeof(Users));
            return users;
        }

        private IReservations GetReservationsFromDataSource()
        {
            DataMapper converterXML = new DataMapper();
            IReservations reservations;
            reservations = (IReservations)converterXML.DeserializeObject(GetDataSource().Element("Reservations").ToString(), typeof(Reservations));
            return reservations;
        }

        private IBooks GetBooksFromDataSource()
        {
            DataMapper converterXML = new DataMapper();
            IBooks books;
            books = (IBooks)converterXML.DeserializeObject(GetDataSource().Element("Books").ToString(), typeof(Books));
            return books;
        }

    }
}

