namespace Application.Dto;

public class BookSearchResult
{
    public List<BookDto> Books { get; set; } = [];
    public int Page { get; set; }
    public int Pages { get; set; }
    public int PageLimit { get; set; }
    public int ItemsCount { get; set; }
}
