namespace Domain.Entities;

public class ExhibitionBook
{
    public int ExhibitionId { get; set; }
    public Exhibition Exhibition { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }
}