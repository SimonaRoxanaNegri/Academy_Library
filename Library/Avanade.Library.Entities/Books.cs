using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Avanade.Library.Entities
{
    [XmlRoot("Books")]
    public class Books : IBooks
    {
        [XmlElement("Book")]
        public List<Book> listOf { get; set; }

        public Books(List<Book> books)
        {
            this.listOf = books;
        }
        public Books()
        {
        }
    }
}
