using MyBooks.Domain.Entity;

namespace MyBooks.DAL.Interfaces;

public interface IAuthorRepository<T>
{
    Task AddAuthor(T entity);

    IQueryable<T> GetAll();

    Task Delete(T entity);

    Task<T> Update(T entity);
    Task<int?> GetAuthorIdByLastName(string authorLastName);

    Task<AuthorEntity> GetAuthorId(int authorId);

}