

using System;

namespace Avanade.Library.Entities
{
    public interface IBooksAvailables
    {
       

        int BookId { get; set; }
       
        string Title { get; set; }

        string AuthorName { get; set; }

        string AuthorSurName { get; set; }

        string PublishingHouse { get; set; }

        int Quantity { get; set; }

        DateTime DateAvailable { get; set; }

        string FormatterBookAvailableString();



    }
}
