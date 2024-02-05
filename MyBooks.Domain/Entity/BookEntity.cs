using MyBooks.Domain.Enum;

namespace MyBooks.Domain.Entity;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public Genre Genre { get; set; }
    public int AuthorId { get; set; }

}