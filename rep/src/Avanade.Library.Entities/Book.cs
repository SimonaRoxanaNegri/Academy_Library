using System.Xml.Serialization;

namespace Avanade.Library.Entities
{
    [XmlRoot(ElementName = "Book")]
    public class Book : IBook
    {

        [XmlAttribute(AttributeName = "BookId")]
        public int BookId { get; set; }

        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "AuthorName")]
        public string AuthorName { get; set; }

        [XmlAttribute(AttributeName = "AuthorSurname")]
        public string AuthorSurname { get; set; }

        [XmlAttribute(AttributeName = "Publisher")]
        public string PublishingHouse { get; set; }

        [XmlAttribute(AttributeName = "Quantity")]
        public int Quantity { get ; set; }

        public Book(int bookId, string title, string authorName, string authorSurname, string publishingHouse, int quantity)
        {
            BookId = bookId;
            Title = title;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            PublishingHouse = publishingHouse;
            Quantity = quantity;
        }

        public Book( string title, string authorName, string authorSurname, string publishingHouse)
        {
            
            Title = title;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            PublishingHouse = publishingHouse;
           
        }
        public Book()
        {

        }
    }
}
