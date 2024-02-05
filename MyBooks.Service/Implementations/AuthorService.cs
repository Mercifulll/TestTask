using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBooks.DAL.Interfaces;
using MyBooks.Domain;
using MyBooks.Domain.Entity;
using MyBooks.Domain.Enum;
using MyBooks.Service.Interfaces;


namespace MyBooks.Service.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository<AuthorEntity> _authorRepository;
        private ILogger<AuthorService> _logger;

        public AuthorService(IAuthorRepository<AuthorEntity> authorRepository,
            ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

    
        public async Task<IBaseResponse<AuthorEntity>> AddAuthor(SaveViewModel model)
        {
            try
            {
                model.Validate();
                _logger.LogInformation($"Add author request - {model.FirstName} {model.LastName}");

                var author = await _authorRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.LastName == model.LastName);

                if (author != null)
                {
                    return new BaseResponse<AuthorEntity>
                    {
                        Description = "Author with the same name already exists",
                        StatusCode = StatusCode.AuthorIsHasAlready
                    };
                }

                author = new AuthorEntity()
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    DateOfBirth = model.DateOfBirth,
                    Books = model.Books
                };

                await _authorRepository.AddAuthor(author);

                _logger.LogInformation($"Author added: {author.FirstName} {author.LastName}");
                return new BaseResponse<AuthorEntity>
                {
                    Description = "Author added successfully",
                    StatusCode = StatusCode.OK,
                    Data = author
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[AuthorService.AddAuthor]: {ex.Message}");
                return new BaseResponse<AuthorEntity>
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        
        public async Task<IBaseResponse<BookEntity>> AddBook(int authorId, SaveViewModel model)
        {
            try
            {
                var author = await _authorRepository.GetAll()
                    .Include(a => a.Books)
                    .FirstOrDefaultAsync(x => x.Id == authorId);

                // Створення нової книги
                var book = new BookEntity
                {
                    Title = model.BookTitle,
                    PageCount = model.PageCount,
                    Genre = model.Genre
                };

                author.Books.Add(book);
                author.BooksCount++;

                // Оновлення автора в базі даних
                await _authorRepository.Update(author);

                // Логування події
                _logger.LogInformation($"Book added: {book.Title} to author {author.FirstName} {author.LastName}");

                return new BaseResponse<BookEntity>
                {
                    Description = "Book added successfully",
                    StatusCode = StatusCode.OK,
                    Data = book
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[AuthorService.AddBook]: {ex.Message}");
                return new BaseResponse<BookEntity>
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        
        public async Task<IBaseResponse<IEnumerable<AuthorViewModel>>> GetAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAll()
                    .Select(x => new AuthorViewModel
                    {
                        Id = x.Id,
                        LastName = x.LastName,
                        BooksCount = x.BooksCount,
                        Created = x.Created
                    })
                    .ToListAsync();

                return new BaseResponse<IEnumerable<AuthorViewModel>>
                {
                    StatusCode = StatusCode.OK,
                    Data = authors
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[AuthorService.GetAuthors]: {ex.Message}");
                return new BaseResponse<IEnumerable<AuthorViewModel>>
                {
                    Description = $"{ex.Message}",
                };
            }
        }

        public Task<int?> GetExistingAuthorIdByLastName(string lastName)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<AuthorEntity>> DeleteAuthor(int authorId)
        {
            try
            {
                var author = await _authorRepository.GetAuthorId(authorId);

                await _authorRepository.Delete(author);

                _logger.LogInformation($"Author deleted: {author.FirstName} {author.LastName}");
                return new BaseResponse<AuthorEntity>
                {
                    Description = "Author deleted successfully",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[AuthorService.DeleteAuthor]: {ex.Message}");
                return new BaseResponse<AuthorEntity>
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        
        public async Task<IBaseResponse<AuthorEntity>> UpdateAuthor(UpdateViewModel model)
        {
            try
            {
                _logger.LogInformation($"Edit author request - {model.AuthorFirstName} {model.AuthorLastName}");

                var author = await _authorRepository.GetAuthorId(model.AuthorId);

                author = new AuthorEntity()
                {
                    LastName = model.AuthorLastName,
                    FirstName = model.AuthorFirstName,
                    MiddleName = model.AuthorMiddleName,
                    DateOfBirth = model.AuthorDateOfBirth,
                };
                 
                await _authorRepository.Update(author);

                _logger.LogInformation($"Author updated: {author.FirstName} {author.LastName}");
                return new BaseResponse<AuthorEntity>
                {
                    Description = "Author updated successfully",
                    StatusCode = StatusCode.OK,
                    Data = author
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[AuthorService.UpdateAuthor]: {ex.Message}");
                return new BaseResponse<AuthorEntity>
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        
        public async Task<AuthorEntity> GetAuthorById(int authorId)
        {
            return await _authorRepository.GetAuthorId(authorId);
        }
        
        public async Task<DetailsViewModel> GetAuthorDetails(int authorId)
        {
            try
            {
                var author = await _authorRepository.GetAuthorId(authorId);

                // Assuming AuthorDetailsViewModel has properties for author details and books
                var authorDetails = new DetailsViewModel
                {
                    LastName = author.LastName,
                    FirstName = author.FirstName,
                    MiddleName = author.MiddleName,
                    DateOfBirth = author.DateOfBirth,
                };

                return authorDetails;
            }
            catch (Exception ex)
            {
                // Handle exception (logging, etc.)
                throw;
            }
        }

    }
}
