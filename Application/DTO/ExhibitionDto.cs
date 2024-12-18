namespace Application.DTO;

public class ExhibitionDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<BookDto> Books { get; set; } = [];
}
