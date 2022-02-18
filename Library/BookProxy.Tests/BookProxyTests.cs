using Microsoft.VisualStudio.TestTools.UnitTesting;
using Avanade.Library.Proxy;
using System.Linq;
using System.Threading.Tasks;
using Avanade.Library.Entities;
using Avanade.Library.BusinessLogic;
using Avanade.Library.DAL;


namespace LibraryProxy.Tests
{
    [TestClass()]
    public class BookProxyTests
    {
        readonly IBusinessLogics logic = new BusinessLogics(new DbDal());

        [TestMethod()]
        public async Task GetBooksTest()
        {
            var resultActual = await BookProxy.GetBooks();
            var rusultExpected = logic.SearchBookAvailables("", "", "", "");

            foreach (IBooksAvailables book in resultActual)
            {
                var expectedBook = rusultExpected.First(h => h.BookId == book.BookId);
                Assert.AreEqual(book.Title, expectedBook.Title);
                Assert.AreEqual(book.AuthorName, expectedBook.AuthorName);
                Assert.AreEqual(book.AuthorSurName, expectedBook.AuthorSurName);
                Assert.AreEqual(book.PublishingHouse, expectedBook.PublishingHouse);
            }

        }

        [TestMethod()]
        public async Task GetBooksByTitleTest()
        {
            string Title = "Lenticchie alla julienne sfuse";
            var resultActual = await BookProxy.GetBooksByTitle(Title);
            var rusultExpected = logic.SearchBookAvailables(Title, "", "", "");

            foreach (IBooksAvailables book in resultActual)
            {
                var expectedBook = rusultExpected.First(h => h.BookId == book.BookId);              
                Assert.AreEqual(book.Title, expectedBook.Title);
            }
        }

        [TestMethod()]
        public async Task GetBooksByAuthorNameTest()
        {
            string AuthorName = "Antonio";
            var resultActual = await BookProxy.GetBooksByAuthorName(AuthorName);
            var rusultExpected = logic.SearchBookAvailables("", AuthorName, "", "");

            foreach (IBooksAvailables book in resultActual)
            {
                var expectedBook = rusultExpected.First(h => h.BookId == book.BookId);
                Assert.AreEqual(book.AuthorName, expectedBook.AuthorName);
            }
        }

        [TestMethod()]
        public async Task GetBooksByAuthorSurnameTest()
        {
            string AuthorSurname = "Albanese";
            var resultActual = await BookProxy.GetBooksByAuthorSurname(AuthorSurname);
            var rusultExpected = logic.SearchBookAvailables("", "", AuthorSurname, "");

            foreach (IBooksAvailables book in resultActual)
            {
                var expectedBook = rusultExpected.First(h => h.BookId == book.BookId);
                Assert.AreEqual(book.AuthorSurName, expectedBook.AuthorSurName);
            }
        }

        [TestMethod()]
        public async Task GetBooksByPublishingHouseTest()
        {
            string PublishingHouse = "Feltrinellini";
            var resultActual = await BookProxy.GetBooksByPublishingHouse(PublishingHouse);
            var rusultExpected = logic.SearchBookAvailables("", "", "", PublishingHouse);

            foreach (IBooksAvailables book in resultActual)
            {
                var expectedBook = rusultExpected.First(h => h.BookId == book.BookId);
                Assert.AreEqual(book.PublishingHouse, expectedBook.PublishingHouse);
            }
        }

        [TestMethod()]
        public async Task AddBookTest()
        {
            var bookToAdd = new Book(0,"TestMsTitoloUnico","NomeAutore","CognomeAutore","Mondadori",1);
            await BookProxy.AddBook(bookToAdd);

            var Title = "TestMsTitoloUnico";
            var AuthorName = "NomeAutore";
            var AuthorSurName = "CognomeAutore";
            var PublishingHouse = "Mondadori";

            var resultActual = await BookProxy.GetBooksByTitle(Title);

            foreach (IBooksAvailables book in resultActual)
            {
                Assert.AreEqual(book.Title, Title);
                Assert.AreEqual(book.AuthorName, AuthorName);
                Assert.AreEqual(book.AuthorSurName, AuthorSurName);
                Assert.AreEqual(book.PublishingHouse, PublishingHouse);
            }

        }

        [TestMethod()]
        public async Task DeleteBookTest()
        {
            var bookToDelete = new Book(0, "TitoloUnicoDeleteTest", "NomeAutore", "CognomeAutore", "Mondadori", 1);
            Response<Book> book = await BookProxy.AddBook(bookToDelete);

            var Title = "TitoloUnicoDeleteTest";

            
            var resultActual = await BookProxy.GetBooksByTitle(Title);
            var bookIdTest = resultActual.FirstOrDefault(x => x.Title == Title);

            int BookId = bookIdTest.BookId;

            await BookProxy.DeleteBook(BookId);

            var resultPostDelete = await BookProxy.GetBooksByTitle(Title);

            Assert.AreEqual(0, resultPostDelete.Count);

        }

        [TestMethod()]
        public async Task RequestUpdateBookTest()
        {
            var bookToUpdate = new Book(3,"TitoloUnicoUpdateTest","NomeAutore", "CognomeAutore", "Mondadori",0);
            await BookProxy.RequestUpdateBook(bookToUpdate);

            var Title = "TitoloUnicoUpdateTest";
            var AuthorName = "NomeAutore";
            var AuthorSurName = "CognomeAutore";
            var PublishingHouse = "Mondadori";

            var resultActual = await BookProxy.GetBooksByTitle(Title);

            foreach (IBooksAvailables book in resultActual)
            {
                Assert.AreEqual(book.Title, Title);
                Assert.AreEqual(book.AuthorName, AuthorName);
                Assert.AreEqual(book.AuthorSurName, AuthorSurName);
                Assert.AreEqual(book.PublishingHouse, PublishingHouse);
            }

        }
    }
}