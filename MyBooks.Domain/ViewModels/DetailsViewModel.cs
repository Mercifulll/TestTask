using System.ComponentModel.DataAnnotations;
using MyBooks.Domain.Entity;
using MyBooks.Domain.Enum;

namespace MyBooks.Domain;

public class DetailsViewModel
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    
}