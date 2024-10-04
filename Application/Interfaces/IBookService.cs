using Application.Dto;

namespace Application.interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllBooksAsync();
        Task<BookDTO> GetBookByIdAsync(int id);
        Task<BookDTO> CreateBookAsync(BookDTO BookDTO);
        Task<BookDTO> UpdateBookAsync(int id, BookDTO BookDTO);
        Task<bool> DeleteBookAsync(int id);
    }
}