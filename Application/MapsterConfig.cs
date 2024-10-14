using Application.Dto;
using Domain.Entities;
using Mapster;

namespace Application;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<ClientCardDto, ClientCard>.NewConfig()
            .IgnoreNullValues(true);
    }
}
