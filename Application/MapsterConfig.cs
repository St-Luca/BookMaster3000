using Application.DTO;
using Domain.Entities;
using Mapster;

namespace Application;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<ClientCard, ClientCardDto>
            .NewConfig()
            .Map(dest => dest.IssuedBooks, 
                 src => src.Issues.Select(issue => issue.Adapt<CirculationRecord>()).ToList())
            .Map(dest => dest.ReturnedBooks, 
                 src => src.Returns.Select(ret => ret.Adapt<CirculationRecord>()).ToList());

        TypeAdapterConfig<Book, BookDto>
            .NewConfig()
            .Map(dest => dest.BookAuthors, src => src.BookAuthors.Select(ba => ba.Author.Name).ToList())
            .Map(dest => dest.BookSubjects, src => src.BookSubjects.Select(bs => bs.Subject.Name).ToList())
            .Map(dest => dest.BookCovers, src => src.Covers.Select(c => c.ImageUrl).ToList());

        TypeAdapterConfig<BookDto, Book>
            .NewConfig()
            .Map(dest => dest.BookAuthors, src => src.BookAuthors.Select(name => new BookAuthor { Author = new Author { Name = name } }).ToList())
            .Map(dest => dest.BookSubjects, src => src.BookSubjects.Select(name => new BookSubject { Subject = new Subject { Name = name } }).ToList())
            .Map(dest => dest.Covers, src => src.BookCovers.Select(cover => new Cover { ImageUrl = cover  }).ToList());

        TypeAdapterConfig<Issue, CirculationRecord>
            .NewConfig()
            .Map(dest => dest.BookId, src => src.BookId)
            .Map(dest => dest.BookTitle, src => src.Book.Title)
            .Map(dest => dest.BookSubtitle, src => src.Book.Subtitle)
            .Map(dest => dest.IssueFrom, src => src.IssueFrom)
            .Map(dest => dest.IssueTo, src => src.IssueTo)
            .Map(dest => dest.ReturnDate, src => src.ReturnDate);

        TypeAdapterConfig<Exhibition, ExhibitionDto>
            .NewConfig()
            .Map(dest => dest.Books,
                 src => src.ExhibitionBooks != null
                    ? src.ExhibitionBooks.Select(eb => eb.Book.Adapt<BookDto>()).ToList()
                    : new List<BookDto>())
            .Map(dest => dest.CreatedDate, src => src.CreatedDate.HasValue ? src.CreatedDate.Value.ToUniversalTime() : (DateTime?)null);

        TypeAdapterConfig<ExhibitionDto, Exhibition>
        .NewConfig()
        .Map(dest => dest.ExhibitionBooks, 
            src => src.Books != null 
                ? src.Books.Select(bookDto => new ExhibitionBook 
                {
                    BookId = bookDto.Id,
                    Book = bookDto.Adapt<Book>(),
                    ExhibitionId = src.Id
                }).ToList() 
                : new List<ExhibitionBook>())
        .Map(dest => dest.CreatedDate, 
            src => src.CreatedDate.HasValue 
                ? src.CreatedDate.Value.ToUniversalTime() 
                : (DateTime?)null);
        }
}