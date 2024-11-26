using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookmasterAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExhibitionController(IExhibitionService _exhibitionService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetExhibitionById(int id)
    {
        var exhibition = await _exhibitionService.GetExhibition(id);

        return Ok(exhibition ?? new ExhibitionDto());
    }

    [HttpGet]
    public async Task<IActionResult> GetExhibitions(int page = 1)
    {
        var result = await _exhibitionService.GetExhibitions(page);

        if (result.Exhibitions == null || result.Exhibitions.Count == 0)
        {
            return Ok(new ExhibitionResponse
            {
                Exhibitions = new List<ExhibitionDto>(),
                ItemsCount = 0,
                Page = page,
                Pages = page,
                PageLimit = _exhibitionService.PaginationLimit
            });
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExhibition(ExhibitionDto exhibitionDto)
    {
        var result = await _exhibitionService.CreateExhibition(exhibitionDto);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Exhibition);
    }

    [HttpPost("{bookId}/{exhibitionId}/addBook")]
    public async Task<IActionResult> AddBookInExhibition(int bookId, int exhibitionId)
    {
        var result = await _exhibitionService.AddBookInExhibition(bookId, exhibitionId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }

    [HttpPost("{bookId}/{exhibitionId}/removeBook")]
    public async Task<IActionResult> RemoveBookFromExhibition(int bookId, int exhibitionId)
    {
        var result = await _exhibitionService.RemoveBookFromExhibition(bookId, exhibitionId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }
}
