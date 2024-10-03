using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly LibraryContext _context;

    public LoansController(LibraryContext context)
    {
        _context = context;
    }
}