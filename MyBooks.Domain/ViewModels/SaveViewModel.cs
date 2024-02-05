using System.ComponentModel.DataAnnotations;
using MyBooks.Domain.Entity;
using MyBooks.Domain.Enum;

namespace MyBooks.Domain;

public class SaveViewModel
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int BooksCount { get; set; } = 0;
    public int AuthorId { get; set; }
    public List<BookEntity> Books { get; set; }
    public string BookTitle { get; set; }
    public int PageCount { get; set; }
    
    [Required(ErrorMessage = "Please select a genre")]
    public Genre Genre { get; set; }
    
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(LastName))
        {
            throw new ArgumentNullException(LastName, "Enter the last name of author please");
        }
        if (string.IsNullOrWhiteSpace(FirstName))
        {
            throw new ArgumentNullException(FirstName, "Enter the first name of author please");
        }
        if (DateOfBirth == default || DateOfBirth > DateTime.Now)
        {
            throw new ArgumentException("Invalid date of birth", nameof(DateOfBirth));
        }
    }

}