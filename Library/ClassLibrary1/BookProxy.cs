using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Avanade.Library.Entities;
using System.Text.Json;
using System.Text;

namespace Avanade.Library.Proxy
{
    public static class BookProxy
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string HttpBasePath = "http://localhost:85/api/book";
        private static readonly string MimeType = "application/json";


        public static async Task<List<BooksAvailables>> GetBooks()
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetBooks");
            var books = await JsonSerializer.DeserializeAsync<List<BooksAvailables>>(await streamTask.Content.ReadAsStreamAsync());
            return books;
        }

        public static async Task <List<BooksAvailables>> GetBooksByTitle(string Title)
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetBooksByTitle?Title=" + Title);
            var books = await JsonSerializer.DeserializeAsync<List<BooksAvailables>>(await streamTask.Content.ReadAsStreamAsync());
            return books;
        }
        public static async Task<List<BooksAvailables>> GetBooksByAuthorName(string AuthorName)
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetBooksByAuthorName?AuthorName=" + AuthorName);
            var books = await JsonSerializer.DeserializeAsync<List<BooksAvailables>>(await streamTask.Content.ReadAsStreamAsync());
            return books;
        }
        public static async Task<List<BooksAvailables>> GetBooksByAuthorSurname(string AuthorSurname)
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetBooksByAuthorSurname?AuthorSurname=" + AuthorSurname);
            var books = await JsonSerializer.DeserializeAsync<List<BooksAvailables>>(await streamTask.Content.ReadAsStreamAsync());
            return books;
        }
        public static async Task<List<BooksAvailables>> GetBooksByPublishingHouse(string PublishingHouse)
        {
            HttpResponseMessage streamTask = await client.GetAsync(HttpBasePath + "/GetBooksByPublishingHouse?PublishingHouse=" + PublishingHouse);
            var books = await JsonSerializer.DeserializeAsync<List<BooksAvailables>>(await streamTask.Content.ReadAsStreamAsync());
            return books;
        }

        public static async Task<Response<Book>> AddBook(Book content)
        {
            string json = JsonSerializer.Serialize<IBook>(content);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, MimeType);
            HttpResponseMessage streamTask = await client.PostAsync(HttpBasePath + "/AddBook", stringContent);
            var bookAdded = await JsonSerializer.DeserializeAsync<Response<Book>>(await streamTask.Content.ReadAsStreamAsync());
            return bookAdded;
        }

        public static async Task<Response<Book>> DeleteBook(int BookId)
        {
            HttpResponseMessage streamTask = await client.DeleteAsync(HttpBasePath + "/DeleteBook?BookId=" + BookId);
            var bookDeleted = await JsonSerializer.DeserializeAsync<Response<Book>>(await streamTask.Content.ReadAsStreamAsync());
            return bookDeleted;
        }

        public static async Task<Response<Book>> RequestUpdateBook(Book content)
        {
            string json = JsonSerializer.Serialize<IBook>(content);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, MimeType);
            HttpResponseMessage streamTask = await client.PostAsync(HttpBasePath + "/RequestUpdateBook", stringContent);
            Response<Book> bookUpdated = await JsonSerializer.DeserializeAsync<Response<Book>>(await streamTask.Content.ReadAsStreamAsync());
            return bookUpdated;
        }

    }
}
