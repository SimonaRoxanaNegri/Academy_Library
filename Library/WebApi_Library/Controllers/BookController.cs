using Avanade.Library.BusinessLogic;
using Avanade.Library.DAL;
using Avanade.Library.Entities;
using System.Collections.Generic;
using System.Web.Http;


namespace WebApi_Library.Controllers
{
    
    public class BookController : ApiController
    {

        readonly IBusinessLogics logic = new BusinessLogics(new DbDal());


        [HttpGet]
        public List<IBooksAvailables> GetBooks()
        {
            return logic.SearchBookAvailables("", "", "", "");
        }

        [HttpGet]
        public List<IBooksAvailables> GetBooksByTitle([FromUri] string Title)
        {
            return logic.SearchBookAvailables(Title, "", "", "");
        }

        [HttpGet]
        public List<IBooksAvailables> GetBooksByAuthorName([FromUri] string AuthorName)
        {
            return logic.SearchBookAvailables("", AuthorName, "", "");
        }

        [HttpGet]
        public List<IBooksAvailables> GetBooksByAuthorSurname([FromUri] string AuthorSurname)
        {
            return logic.SearchBookAvailables("", "" ,AuthorSurname, "");
        }

        [HttpGet]
        public List<IBooksAvailables> GetBooksByPublishingHouse([FromUri] string PublishingHouse)
        {
            return logic.SearchBookAvailables("", "", "", PublishingHouse);
        }

        [HttpPost]
        public IResponse<IBook> AddBook([FromBody] Book book)
        {
            return logic.RequestAddBook(book.Title, book.AuthorName, book.AuthorSurname, book.PublishingHouse, book.Quantity);
        }

        [HttpPost]
        public IResponse<IBook> RequestUpdateBook([FromBody] Book book) 
        {
            return logic.RequestUpdateBook(book.BookId, book.Title, book.AuthorName, book.AuthorSurname, book.PublishingHouse);
        }

        [HttpDelete]
        public IResponse<IBook> DeleteBook([FromUri] int BookId)
        {
            return logic.RequestDeleteBook(BookId);
        }


    }
}