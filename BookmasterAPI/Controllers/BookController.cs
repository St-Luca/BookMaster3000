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
    public ActionResult<List<BookDto>> SearchBooks(string title = null, string author = null, string subject = null)
    {
        var books = _bookService.FindBooks(title, author, subject);
        if (books == null || !books.Any())
            return Ok(new List<BookDto>());
        return Ok(books);
    }

}
