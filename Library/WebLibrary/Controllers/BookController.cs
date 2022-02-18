using Avanade.Library.Entities;
using Avanade.Library.Proxy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace WebLibrary.Controllers
{
    public class BookController : Controller
    {

        public async Task<ActionResult> Index(string title, string author, string publisher)
        {
            List<BooksAvailables> ListOfbooks = await BookProxy.GetBooks();
            
            if (!String.IsNullOrEmpty(title))
            {
                ListOfbooks = ListOfbooks.FindAll(s => s.Title.ToLower().Contains(title.ToLower()));
            }

            if (!String.IsNullOrEmpty(author))
            {
                ListOfbooks = ListOfbooks.FindAll(s => s.AuthorName.ToLower().Contains(author.ToLower()) || s.AuthorSurName.ToLower().Contains(author.ToLower()));
            }

            if (!String.IsNullOrEmpty(publisher))
            {
                ListOfbooks = ListOfbooks.FindAll(s => s.PublishingHouse.ToLower().Contains(publisher.ToLower()));
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("books_partialview", ListOfbooks);
            }
            
            return View(ListOfbooks);
        }


        public async Task<ActionResult> BooksAvailables()
        {
            List<BooksAvailables> BooksAvaialablesList = await BookProxy.GetBooks();
            var booksAvailablesNotReserved = BooksAvaialablesList.Where(x => x.DateAvailable < DateTime.Now);
            return View(booksAvailablesNotReserved);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> Create(Book book)
        {
            await BookProxy.AddBook(book);
            return View(book);
        }

        
        public async Task<ActionResult> Details(int id)
        {
            List<BooksAvailables> bookList = await BookProxy.GetBooks();
            BooksAvailables book = bookList.FindLast(bookAvailable => bookAvailable.BookId == id);
            return View(book);
        }

        
        public async Task<ActionResult> Edit(int id)
        {
            List<BooksAvailables> bookList = await BookProxy.GetBooks();
            BooksAvailables book = bookList.FindLast(bookAvailable => bookAvailable.BookId == id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BooksAvailables book)
        {
            Response<Book> bookToUpdate = await BookProxy.RequestUpdateBook(new Book(book.BookId, book.Title, book.AuthorName, book.AuthorSurName, book.PublishingHouse, book.Quantity));
            return View(bookToUpdate.entity);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            List<BooksAvailables> bookList = await BookProxy.GetBooks();
            var result = bookList.FindLast(bookAvailable => bookAvailable.BookId == id);
            Response<Book> deleteResult = new Response<Book>();
            if(result != null)
            {
                deleteResult = await BookProxy.DeleteBook(result.BookId);
                if (deleteResult.ResponseMessage.Contains("successo"))
                {
                    bookList = await BookProxy.GetBooks();
                }
            }
            return PartialView("~/Views/Shared/books_partialview.cshtml", bookList);
        }

        /*[HttpPost]
        public async Task<ActionResult> ConfirmDelete(int id)
        {
            var book = await BookProxy.ViewBookById(id);
            await BookProxy.RemoveBook(book);
            var books = await BookProxy.ViewAllBooks();
            return PartialView("books_partialview", ListOfbooks);
        }*/

        /*public ActionResult Delete(int ? id)
        {
            BooksAvailables book = (Task.Run(() => BookProxy.GetBooks()).Result).FindLast(bookAvailable => bookAvailable.BookId == id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Task.Run(() => BookProxy.DeleteBook(id));
            return RedirectToAction("Index");
        }
        */




    }
}