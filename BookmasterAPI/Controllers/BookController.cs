using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Application.interfaces;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("search")]
    public ActionResult<List<BookDto>> SearchBooks(string title, string author, string subject)
    {
        var books = _bookService.FindBooks(title, author, subject);
        if (books == null || !books.Any())
            return NotFound("No books found with the provided criteria.");
        return Ok(books);
    }

}
