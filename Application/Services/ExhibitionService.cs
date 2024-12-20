﻿using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Mapster;
using Persistence.Interfaces;
using Persistence.Repositories;

namespace Application.Services;

public class ExhibitionService(
    IExhibitionRepository _exhibitionRepository,
    IBookRepository _bookRepository,
    IExhibitionBookRepository _exhibitionBookRepository) : IExhibitionService
{
    public byte PaginationLimit => 50;

    public async Task<(bool IsSuccess, string Message)> AddBookInExhibition(int bookId, int exhibitionId)
    {
        var exhibition = await _exhibitionRepository.GetExhibition(exhibitionId);

        if (exhibition != null)
        {
            var book = await _bookRepository.GetBook(bookId);

            if(book != null)
            {
                var exhibitionBook = new ExhibitionBook
                {
                    ExhibitionId = exhibitionId,
                    BookId = bookId,
                    Exhibition = exhibition,
                    Book = book
                };

                exhibition.ExhibitionBooks.Add(exhibitionBook);
                
                await _exhibitionBookRepository.AddExhibitionBook(exhibitionBook);
                await _exhibitionRepository.EditExhibition(exhibition);

                return (true, string.Empty);
            }

            return (false, "Book not found");
        }

        return (false, "Exhibition not found");
    }

    public async Task<(bool IsSuccess, string Message, ExhibitionDto? Exhibition)> CreateExhibition(ExhibitionDto exhibitionDto)
    {
        if (exhibitionDto != null && exhibitionDto.Name != "")
        {
            var newExhibition = exhibitionDto.Adapt<Exhibition>();
            newExhibition.CreatedDate = DateTime.Now.ToUniversalTime();

            await _exhibitionRepository.CreateExhibition(newExhibition);

            return (true, string.Empty, exhibitionDto);
        }

        return (false, "Invalid exhibition data", exhibitionDto);
    }

    public async Task<ExhibitionDto?> GetExhibition(int id)
    {
        var exhibition = await _exhibitionRepository.GetExhibition(id);

        if (exhibition == null)
            return null;

        return exhibition.Adapt<ExhibitionDto>();
    }

    public async Task<ExhibitionResponse> GetExhibitions(int page)
    {
        var exhibitions = await _exhibitionRepository.GetExhibitions();

        return Paginate(exhibitions, page);
    }

    public async Task<(bool IsSuccess, string Message)> RemoveBookFromExhibition(int bookId, int exhibitionId)
    {
        var exhibition = await _exhibitionRepository.GetExhibition(exhibitionId);

        if (exhibition != null)
        {
            var book = await _bookRepository.GetBook(bookId);

            if (book != null)
            {
                var exhibitionBook = exhibition.ExhibitionBooks
                    .FirstOrDefault(eb => eb.BookId == bookId);

                if (exhibitionBook != null)
                {
                    exhibition.ExhibitionBooks.Remove(exhibitionBook);

                    await _exhibitionBookRepository.RemoveExhibitionBook(exhibitionBook);
                    await _exhibitionRepository.EditExhibition(exhibition);

                    return (true, string.Empty);
                }

                return (false, "Book not associated with the exhibition");
            }

            return (false, "Book not found");
        }

        return (false, "Exhibition not found");
    }

    private ExhibitionResponse Paginate(List<Exhibition> exhibitions, int page)
    {
        var isFullListRequested = page == -1;

        var paginatedExhibitions = isFullListRequested
            ? exhibitions
            : exhibitions.Skip((page - 1) * PaginationLimit).Take(PaginationLimit);

        return new ExhibitionResponse
        {
            Exhibitions = paginatedExhibitions.Select(b => b.Adapt<ExhibitionDto>()).ToList(),
            ItemsCount = exhibitions.Count,
            PageLimit = PaginationLimit,
            Page = page,
            Pages = (exhibitions.Count + PaginationLimit - 1) / PaginationLimit,
        };
    }
}
