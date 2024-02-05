using MyBooks.Domain;
using MyBooks.Domain.Entity;

namespace MyBooks.Service.Interfaces;

public interface IAuthorService
{
    Task<IBaseResponse<AuthorEntity>> AddAuthor(SaveViewModel model);
    Task<IBaseResponse<BookEntity>> AddBook(int authorId, SaveViewModel model);
    Task<IBaseResponse<IEnumerable<AuthorViewModel>>> GetAuthors();
    Task<IBaseResponse<AuthorEntity>> DeleteAuthor(int authorId);
    Task<int?> GetExistingAuthorIdByLastName(string lastName);
    Task<IBaseResponse<AuthorEntity>> UpdateAuthor(UpdateViewModel model);
    Task<AuthorEntity> GetAuthorById(int authorId);
    
    Task<DetailsViewModel> GetAuthorDetails(int authorId);

}