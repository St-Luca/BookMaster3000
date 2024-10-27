using Application.Dto;
using Domain.Entities;
using Mapster;

namespace Application;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<ClientCardDto, ClientCard>.NewConfig()
            .TwoWays()
            .IgnoreNullValues(true);

        TypeAdapterConfig<Book, BookDto>
            .NewConfig()
            .Map(dest => dest.BookAuthors, src => src.BookAuthors.Select(ba => ba.Author.Name).ToList())
            .Map(dest => dest.BookSubjects, src => src.BookSubjects.Select(bs => bs.Subject.Name).ToList());
    }
}