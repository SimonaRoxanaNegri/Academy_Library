using Microsoft.VisualStudio.TestTools.UnitTesting;
using Avanade.Library.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avanade.Library.BusinessLogic;
using Avanade.Library.DAL;
using Avanade.Library.Entities;

namespace LibraryProxy.Tests
{
    [TestClass()]
    public class ReservationProxyTests
    {
        readonly IBusinessLogics logic = new BusinessLogics(new DbDal());

        [TestMethod()]
        public async Task DeleteReservationTest()
        {
            IReservation reservationToAdd = new Reservation(0, 1, 20, DateTime.Now, DateTime.Now.AddDays(30));
            await ReservationProxy.AddReservation((Reservation)reservationToAdd);
  
            var NewReservationList = logic.RequestReservationHistory(0, 0, 0);
            var LastReservation = NewReservationList.OrderBy(x => x.ReservationId).Last();

            await ReservationProxy.DeleteReservation(LastReservation.ReservationId);
  
            var resultExpected = logic.RequestReservationHistory(0, 0, 0);
            var reservationExpected = resultExpected.OrderBy(x => x.ReservationId).Last();
            Assert.AreEqual(reservationExpected.EndDate.Date, DateTime.Now.Date);

        }

        [TestMethod()]
        public async Task AddReservationTest()
        {
            IReservation reservationToAdd = new Reservation(0,1,34,DateTime.Now,DateTime.Now.AddDays(30));
            var resultActual = await ReservationProxy.AddReservation((Reservation)reservationToAdd);
            //var rusultExpected = logic.RequestReservation(reservation);
            var listBooks = await ReservationProxy.GetReservationHistory();
            var resultExpected = logic.RequestReservationHistory(0, 0, 0);

            foreach (IReservationsHistory book in listBooks)
            {
                var expectedBookReserved = resultExpected.First(h => h.ReservationId == book.ReservationId);
                Assert.AreEqual(book.Reserved, expectedBookReserved.Reserved);
            }
        }

        [TestMethod()]
        public async Task GetReservationHistoryTest()
        {
            var resultActual = await ReservationProxy.GetReservationHistory();
            var resultExpected = logic.RequestReservationHistory(0, 0, 0);

            foreach (IReservationsHistory book in resultActual)
            {
                var expectedBook = resultExpected.First(h => h.ReservationId == book.ReservationId);
                Assert.AreEqual(book.UserId, expectedBook.UserId);
                Assert.AreEqual(book.BookId, expectedBook.BookId);
                Assert.AreEqual(book.Title, expectedBook.Title);
                Assert.AreEqual(book.Username, expectedBook.Username);
                Assert.AreEqual(book.StartDate, expectedBook.StartDate);
                Assert.AreEqual(book.EndDate, expectedBook.EndDate);
                Assert.AreEqual(book.ReservationId, expectedBook.ReservationId);
                Assert.AreEqual(book.Quantity, expectedBook.Quantity);
                Assert.AreEqual(book.Reserved, expectedBook.Reserved);
            }

        }

        [TestMethod()]
        public async Task GetReservationHistoryByBookIdTest()
        {
            int BookId = 1;
            var resultActual = await ReservationProxy.GetReservationHistoryByBookId(BookId);
            var resultExpected = logic.RequestReservationHistory(BookId, 0, 0);

            foreach (IReservationsHistory book in resultActual)
            {
                var expectedBook = resultExpected.First(h => h.BookId == book.BookId);
                Assert.AreEqual(book.UserId, expectedBook.UserId);
                Assert.AreEqual(book.BookId, expectedBook.BookId);
                Assert.AreEqual(book.Title, expectedBook.Title);
                Assert.AreEqual(book.Username, expectedBook.Username);
                Assert.AreEqual(book.StartDate, expectedBook.StartDate);
                Assert.AreEqual(book.EndDate, expectedBook.EndDate);
                Assert.AreEqual(book.ReservationId, expectedBook.ReservationId);
                Assert.AreEqual(book.Quantity, expectedBook.Quantity);
                Assert.AreEqual(book.Reserved, expectedBook.Reserved);
            }
        }

        [TestMethod()]
        public async Task GetReservationHistoryByUserIdTest()
        {
            int UserId = 2;
            var resultActual = await ReservationProxy.GetReservationHistoryByUserId(UserId);
            var resultExpected = logic.RequestReservationHistory(0, UserId, 0);

            foreach (IReservationsHistory book in resultActual)
            {
                var expectedBook = resultExpected.First(h => h.ReservationId == book.ReservationId);
                Assert.AreEqual(book.UserId, expectedBook.UserId);
                Assert.AreEqual(book.BookId, expectedBook.BookId);
                Assert.AreEqual(book.Title, expectedBook.Title);
                Assert.AreEqual(book.Username, expectedBook.Username);
                Assert.AreEqual(book.StartDate, expectedBook.StartDate);
                Assert.AreEqual(book.EndDate, expectedBook.EndDate);
                Assert.AreEqual(book.ReservationId, expectedBook.ReservationId);
                Assert.AreEqual(book.Quantity, expectedBook.Quantity);
                Assert.AreEqual(book.Reserved, expectedBook.Reserved);
            }
        }

        [TestMethod()]
        public async Task GetReservationHistoryByReservationIdTest()
        {
            int ReservationId = 33;
            var resultActual = await ReservationProxy.GetReservationHistoryByReservationId(ReservationId);
            var resultExpected = logic.RequestReservationHistory(0, 0, ReservationId);

            foreach (IReservationsHistory book in resultActual)
            {
                var expectedBook = resultExpected.First(h => h.ReservationId == book.ReservationId);
                Assert.AreEqual(book.UserId, expectedBook.UserId);
                Assert.AreEqual(book.BookId, expectedBook.BookId);
                Assert.AreEqual(book.Title, expectedBook.Title);
                Assert.AreEqual(book.Username, expectedBook.Username);
                Assert.AreEqual(book.StartDate, expectedBook.StartDate);
                Assert.AreEqual(book.EndDate, expectedBook.EndDate);
                Assert.AreEqual(book.ReservationId, expectedBook.ReservationId);
                Assert.AreEqual(book.Quantity, expectedBook.Quantity);
                Assert.AreEqual(book.Reserved, expectedBook.Reserved);
            }
        }
    }
}