using BookStore.API.Domain;

namespace BookStore.Persistence.Repository;

public interface IBookRepository : IRepository<Book>
{
    public Task<bool> UpdateAsync(Book entity);
}