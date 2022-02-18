using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;


namespace Avanade.Library.Entities
{
    [XmlRoot(ElementName = "BookAvailable")]
    public class BooksAvailables : IBooksAvailables
    {

        [XmlAttribute(AttributeName = "BookId")]
        public int BookId { get; set; }

        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "AuthorName")]
        public string AuthorName { get; set; }

        [XmlAttribute(AttributeName = "AuthorSurName")]
        public string AuthorSurName { get; set; }

        [XmlAttribute(AttributeName = "PublishingHouse")]
        public string PublishingHouse { get; set; }

        [XmlAttribute(AttributeName = "Quantity")]
        public int Quantity { get; set; }

        [XmlAttribute(AttributeName = "DateAvailable")]
        public DateTime DateAvailable { get; set; }


        public BooksAvailables(int bookId, string title, string authorName, string authorSurName, string publishingHouse, DateTime dateAvailable)
        { 
            Title = title;
            AuthorName = authorName;
            AuthorSurName = authorSurName;
            PublishingHouse = publishingHouse;
            DateAvailable = dateAvailable;
            BookId = bookId;
        }
        public BooksAvailables()
        {
        }

        public string FormatterBookAvailableString()
        {
            return $"\nCodice = {this.BookId},\nTitolo = {this.Title},\nNome Autore = {this.AuthorName},\nCognome Autore = {this.AuthorSurName},\nCasa Editrice = {this.PublishingHouse},\nData disponibilità = {this.DateAvailable.ToString("dd/MM/yyyy")}";
        }
        

    }
}

