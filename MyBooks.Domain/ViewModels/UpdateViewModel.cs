using MyBooks.Domain.Enum;

namespace MyBooks.Domain;

public class UpdateViewModel
{
    // Властивості для оновлення автора
    public int AuthorId { get; set; }
    public string AuthorLastName { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorMiddleName { get; set; }
    public DateTime AuthorDateOfBirth { get; set; }

    // Властивості для оновлення книги
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public int BookPageCount { get; set; }
    public Genre BookGenre { get; set; }
    
    // public void Validate()
    // {
    //     if (string.IsNullOrWhiteSpace(AuthorLastName))
    //     {
    //         throw new ArgumentNullException(AuthorLastName, "Enter the last name of author please");
    //     }
    //     if (string.IsNullOrWhiteSpace(AuthorFirstName))
    //     {
    //         throw new ArgumentNullException(AuthorFirstName, "Enter the first name of author please");
    //     }
    //     if (AuthorDateOfBirth == default || AuthorDateOfBirth > DateTime.Now)
    //     {
    //         throw new ArgumentException("Invalid date of birth", nameof(AuthorDateOfBirth));
    //     }
    // }

}