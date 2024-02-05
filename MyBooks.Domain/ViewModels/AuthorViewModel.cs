using System.ComponentModel.DataAnnotations;
using MyBooks.Domain.Entity;

namespace MyBooks.Domain;

public class AuthorViewModel
{
    public int Id { get; set; }
    [Display(Name = "Author name")]
    public string LastName { get; set; }
    public int BooksCount { get; set; } = 0;
    public DateTime Created { get; set; }
}