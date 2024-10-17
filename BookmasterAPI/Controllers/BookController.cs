using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService _bookService) : ControllerBase
{
    [HttpGet("search")]
    public ActionResult<List<BookDto>> SearchBooks(string title = null, string author = null, string subject = null)
    {
        var books = _bookService.FindBooks(title, author, subject);

        if (books == null || !books.Any())
            return Ok(new List<BookDto>());
        return Ok(books);
    }

}
