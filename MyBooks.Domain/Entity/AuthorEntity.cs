namespace MyBooks.Domain.Entity;

public class AuthorEntity
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<BookEntity> Books { get; set; }
    public int BooksCount { get; set; } = 0;
    public DateTime Created { get; set; }
}