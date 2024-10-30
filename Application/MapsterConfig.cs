using Application.Dto;
using Application.DTO;
using Domain.Entities;
using Mapster;

namespace Application;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<ClientCard, ClientCardDto>.NewConfig()
            .TwoWays()
            .IgnoreNullValues(true)
            .Map(dest => dest.Issues, src => src.Issues.Select(issue => issue.Adapt<CirculationRecord>()).ToList())
            .Map(dest => dest.Returns, src => src.Returns.Select(returnItem => returnItem.Adapt<CirculationRecord>()).ToList());

        TypeAdapterConfig<Book, BookDto>
            .NewConfig()
            .Map(dest => dest.BookAuthors, src => src.BookAuthors.Select(ba => ba.Author.Name).ToList())
            .Map(dest => dest.BookSubjects, src => src.BookSubjects.Select(bs => bs.Subject.Name).ToList());

        TypeAdapterConfig<Issue, CirculationRecord>
            .NewConfig()
            .Map(dest => dest.BookTitle, src => src.Book.Title)
            .Map(dest => dest.IssueFrom, src => src.IssueFrom)
            .Map(dest => dest.IssueTo, src => src.IssueTo)
            .Map(dest => dest.ReturnDate, src => src.ReturnDate);
    }
}