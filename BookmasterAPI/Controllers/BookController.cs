using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService _bookService) : ControllerBase
{
    [HttpGet("search")]
    public ActionResult<BookSearchResult> SearchBooks(string title = null, string author = null, string subject = null, int page = 1)
    {
        var result =  _bookService.FindBooks(title, author, subject, page);

        if (result.Books == null || result.Books.Count == 0)
        {
            return Ok(new BookSearchResult
            {
                Books = new List<BookDto>(),
                ItemsCount = 0,
                Page = page,
                Pages = page,
                PageLimit = _bookService.PaginationLimit
            });
        }

        return Ok(result);
    }
}
