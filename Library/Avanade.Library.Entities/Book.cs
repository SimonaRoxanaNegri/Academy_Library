using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Avanade.Library.Entities
{
    [DataContract]
    [XmlRoot(ElementName = "Book")]
    public class Book : IBook
    {
        [DataMember]
        [XmlAttribute(AttributeName = "BookId")]
        public int BookId { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Title")]
        [Required]
        public string Title { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "AuthorName")]
        [Required]
        public string AuthorName { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "AuthorSurname")]
        [Required]
        public string AuthorSurname { get; set; }

        [DataMember]
        [XmlAttribute(AttributeName = "Publisher")]
        [Required]
        public string PublishingHouse { get; set; }

        [DataMember]
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
