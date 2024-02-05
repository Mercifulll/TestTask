using Microsoft.EntityFrameworkCore;
using MyBooks.DAL.Interfaces;
using MyBooks.Domain.Entity;

namespace MyBooks.DAL.Repositories;

public class AuthorRepository : IAuthorRepository<AuthorEntity>
{
    private readonly AppDbContext _appDbContext;

    public AuthorRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAuthor(AuthorEntity entity)
    {
        await _appDbContext.Authors.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
    }
    public IQueryable<AuthorEntity> GetAll()
    {
        return _appDbContext.Authors;
    }
    public async Task Delete(AuthorEntity entity)
    {
        _appDbContext.Authors.Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }
    public async Task<AuthorEntity> Update(AuthorEntity entity)
    {
        _appDbContext.Authors.Update(entity);
        await _appDbContext.SaveChangesAsync();

        return entity;
    }
    public async Task<int?> GetAuthorIdByLastName(string authorLastName)
    {
        var author = await _appDbContext.Authors
            .FirstOrDefaultAsync(x => x.LastName == authorLastName);

        return author?.Id;
    }
    public async Task<AuthorEntity> GetAuthorId(int authorId)
    {
        return await _appDbContext.Authors.FindAsync(authorId);
    }
    
    public async Task<BookEntity> GetBookById(int id)
    {
        return await _appDbContext.Books.FindAsync(id);
    }
}