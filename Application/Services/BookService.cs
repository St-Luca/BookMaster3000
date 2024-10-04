using Application.Dto;
using Domain.Entities;
using Application.interfaces;
using Persistence.interfaces;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books.Select(MapToDto);
        }

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return book == null ? null : MapToDto(book);
        }

        public async Task<BookDTO> CreateBookAsync(BookDTO BookDTO)
        {
            var book = MapToEntity(BookDTO);
            var createdBook = await _bookRepository.CreateBookAsync(book);
            return MapToDto(createdBook);
        }

        public async Task<BookDTO> UpdateBookAsync(int id, BookDTO BookDTO)
        {
            var book = MapToEntity(BookDTO);
            var updatedBook = await _bookRepository.UpdateBookAsync(id, book);
            return updatedBook == null ? null : MapToDto(updatedBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteBookAsync(id);
        }

        private BookDTO MapToDto(Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                PublicationDate = book.PublicationDate,
                Description = book.Description,
                Author = book.Author
            };
        }

        private Book MapToEntity(BookDTO BookDTO)
        {
            return new Book
            {
                Id = BookDTO.Id,
                Title = BookDTO.Title,
                Subtitle = BookDTO.Subtitle,
                PublicationDate = BookDTO.PublicationDate,
                Description = BookDTO.Description,
                Author = BookDTO.Author
            };
        }
    }
}
