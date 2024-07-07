using BookStore.API.Domain;
using BookStore.Infractructure.Dtos;

namespace BookStore.Services.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(BookDTO bookDTO);
    Task<BookDTO> UpdateBookAsync(int id, BookDTO bookDTO);
    Task<bool> RemoveBookAsync(int id);
}