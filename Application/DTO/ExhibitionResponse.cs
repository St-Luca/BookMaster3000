namespace Application.DTO;

public class ExhibitionResponse
{
    public List<ExhibitionDto> Exhibitions { get; set; } = [];
    public int Page { get; set; }
    public int Pages { get; set; }
    public int PageLimit { get; set; }
    public int ItemsCount { get; set; }
}
