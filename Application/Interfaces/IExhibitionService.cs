using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces;

public interface IExhibitionService
{
    public byte PaginationLimit { get; }
    Task<ExhibitionDto?> GetExhibition(int id);
    Task<ExhibitionResponse> GetExhibitions(int page);
    Task<(bool IsSuccess, string Message, ExhibitionDto? Exhibition)> CreateExhibition(ExhibitionDto exhibitionDto);
    Task<(bool IsSuccess, string Message)> AddBookInExhibition(int bookId, int exhibitionId);
    Task<(bool IsSuccess, string Message)> RemoveBookFromExhibition(int bookId, int exhibitionId);
}
