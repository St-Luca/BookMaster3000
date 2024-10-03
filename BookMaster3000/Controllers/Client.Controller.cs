using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly LibraryContext _context;

    public ClientsController(LibraryContext context)
    {
        _context = context;
    }
}