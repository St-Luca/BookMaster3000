namespace Domain.Entities;

public class Exhibition
{
    public int Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<ExhibitionBook> ExhibitionBooks { get; set; } = [];
}
