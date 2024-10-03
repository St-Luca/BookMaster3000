using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{

    // Метод для поиска книг с фильтрацией по названию, автору и теме
    [HttpGet]
    public ActionResult<IEnumerable<string>> GetBooks(string title = null, string author = null, string subject = null, int page = 1, int pageSize = 10)
    {
        return Ok("Готово");
    }

    // Метод для получения информации о книге по её ID
    [HttpGet("{id}")]
    public ActionResult<string> GetBook(string id)
    {
        return Ok("Готово");
    }
}