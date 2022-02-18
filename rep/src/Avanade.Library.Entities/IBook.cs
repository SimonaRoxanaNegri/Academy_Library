

namespace Avanade.Library.Entities
{
    public interface IBook 
    {
        int BookId { get; set; }
        string Title { get; set; }
        string AuthorName { get; set; }
        string AuthorSurname { get; set; }
        string PublishingHouse { get; set; }
        int Quantity { get; set; }
    }
}
