using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context)
    {
        _context = context;
    }

    // Метод для поиска книг с фильтрацией по названию, автору и теме
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetBooks(string title = null, string author = null, string subject = null, int page = 1, int pageSize = 10)
    {
        var query = _context.Books.AsQueryable();

        // Фильтрация по названию
        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(b => b.Title.Contains(title));
        }

        // Фильтрация по автору
        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.BookAuthors.Any(ba => ba.Author.Name.Contains(author)));
        }

        // Фильтрация по теме
        if (!string.IsNullOrEmpty(subject))
        {
            query = query.Where(b => b.BookSubjects.Any(bs => bs.Subject.Name.Contains(subject)));
        }

        // Пагинация
        var totalItems = query.Count();
        var books = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return Ok(new
        {
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
            CurrentPage = page,
            Books = books
        });
    }

    // Метод для получения информации о книге по её ID
    [HttpGet("{id}")]
    public ActionResult<Book> GetBook(string id)
    {
        var book = _context.Books
            .Where(b => b.Key == id)
            .FirstOrDefault();

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }
}
